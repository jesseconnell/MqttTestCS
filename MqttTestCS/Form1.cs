using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MqttTestCS
{
    public partial class Form1 : Form
    {
        MqttClient client = new MqttClient("127.0.0.1", 1883, false, null, null, MqttSslProtocols.None);
        Dictionary<ushort, string> pendingSubscribes = new Dictionary<ushort, string>();
        Dictionary<ushort, string> pendingUnsubscribes = new Dictionary<ushort, string>();
        bool callbackManagingCheckStates;
        System.Threading.Thread firehoseThread;
        bool firehoseOn = false;
        bool connected;
        bool closing;


        public Form1()
        {
            InitializeComponent();
            client.ConnectionClosed += Client_ConnectionClosed;
            client.MqttMsgPublished += Client_MqttMsgPublished;
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
            client.MqttMsgUnsubscribed += Client_MqttMsgUnsubscribed;
            setConnected(false);

            firehoseThread = new System.Threading.Thread(() =>
            {
                firehose();
            });
            firehoseThread.IsBackground = true;
            firehoseThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.IsConnected)
            {
                stopFirehose();
                closing = true;
                firehoseThread.Join();
                client.Disconnect();
                while (connected)
                {
                    log("Waiting for disconnect...");
                    System.Threading.Thread.Sleep(100);
                }
                log("DISCONNECT ok - goodbye!");
                System.Threading.Thread.Sleep(500);
            }
        }

        void log(string fmt, params object[] args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => {
                    log(fmt, args);
                }));
                return;
            }

            string line = fmt + " (not yet formatted)";
            try
            {
                line = string.Format(fmt, args);
                logTextBox.AppendText(line + System.Environment.NewLine);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error logging '{0}'.  Exception: {1}", line, ex.Message);
            }
        }

        void setConnected(bool b)
        {
            connected = b;
            connectButton.Enabled = !b;
            messageTextBox.Enabled = b;
            publishButton.Enabled = b;
            firehoseButton.Enabled = b;
            topicsCheckListBox.Enabled = b;
        }

        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            setConnected(false);
        }
        private void Client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            log("PUBLISH {0} msg {1}", e.IsPublished ? "ok" : "failed", e.MessageId);
            Invoke(new Action(() =>
            {
                publishButton.Enabled = true;
            }));
        }
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Invoke(new Action(() =>
            {
                log("RECEIVE {0}:  {1}", e.Topic, Encoding.ASCII.GetString(e.Message));
            }));
        }
        private void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                try
                {
                    string topic = pendingSubscribes[e.MessageId];
                    pendingSubscribes.Remove(e.MessageId);
                    log("SUBSCRIBE {0} ok", topic);
                    int index = topicsCheckListBox.Items.IndexOf(topic);
                    if(index != -1)
                    { 
                        callbackManagingCheckStates = true;
                        topicsCheckListBox.SetItemChecked(index, true);
                        topicsCheckListBox.Enabled = true;
                        topicsCheckListBox.Select();
                    }
                }
                catch (Exception ex)
                {
                    log("Caught Exception {0}, message='{1}'", ex.ToString(), ex.Message);
                }
                callbackManagingCheckStates = false;
            }));
        }
        private void Client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                try {

                    string topic = pendingUnsubscribes[e.MessageId];
                    pendingUnsubscribes.Remove(e.MessageId);
                    log("UNSUBSCRIBE {0} ok", topic);
                    int index = topicsCheckListBox.Items.IndexOf(topic);
                    if (index != -1)
                    {
                        callbackManagingCheckStates = true;
                        topicsCheckListBox.SetItemChecked(index, false);
                        topicsCheckListBox.Enabled = true;
                        topicsCheckListBox.Select();
                    }
                }
                catch (Exception ex)
                {
                    log("Caught Exception {0}, message='{1}'", ex.ToString(), ex.Message);
                }
                callbackManagingCheckStates = false;
            }));
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            try
            {
                string clientId = clientIdTextBox.Text == "(auto)"
                    ? Guid.NewGuid().ToString()
                    : clientIdTextBox.Text;

                clientIdTextBox.Text = clientId;

                byte b = client.Connect(clientId, "", "", true, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true,
                    //string.Format("/{0}/will", clientId), "my will",
                    "deaths/{0}", string.Format("{0} just died  :(", clientId),
                    false, 600);

                if (b == 0)
                    log("CONNECT ok");
                else
                    log("CONNECT returned with error: {}", b);

                setConnected(b == 0);
            }
            catch (Exception ex)
            {
                log("Caught Exception {0}, message='{1}'", ex.ToString(), ex.Message);
            }
        }

        void publish(string topic, string message)
        {
            try
            {
                ushort msgId = client.Publish(topic, Encoding.ASCII.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                log("PUBLISH topic: {0} msgId: {1} msg: {2}", topic, msgId, message);
            }
            catch (Exception ex)
            {
                log("Caught Exception {0}, message='{1}'", ex.ToString(), ex.Message);
                topicsCheckListBox.Enabled = true;
            }
        }

        string SelectedTopic
        {
            get {
                return topicsCheckListBox.SelectedIndex == -1 
                    ? null 
                    : topicsCheckListBox.GetItemText(topicsCheckListBox.SelectedItem);
            }
        }

        private void publishButton_Click(object sender, EventArgs e)
        {
            if (SelectedTopic == null)
            {
                log("No topic selected");
                return;
            }
            publishButton.Enabled = false;
            publish(SelectedTopic, messageTextBox.Text);
            publishButton.Enabled = true;
        }

        private void topicsCheckListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (callbackManagingCheckStates) return;
            try
            {
                System.Console.WriteLine("ItemCheck() with sender = {0}, e.cur={1}, e.new={2}, e.idx={3}", sender.ToString(), e.CurrentValue, e.NewValue, e.Index);
                topicsCheckListBox.Enabled = false;
                string topic = topicsCheckListBox.GetItemText(topicsCheckListBox.Items[e.Index]);

                bool subscribing = e.NewValue == CheckState.Checked;
                log("{0} topic {1}", subscribing ? "SUBSCRIBE" : "UNSUBSCRIBE", topic);
                e.NewValue = e.CurrentValue;  // Let subscribe/unsubscribe event manage this

                if (subscribing)
                {
                    ushort msgId = client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                    pendingSubscribes[msgId] = topic;
                }
                else
                {
                    ushort msgId = client.Unsubscribe(new string[] { topic });
                    pendingUnsubscribes[msgId] = topic;
                }
            }
            catch (Exception ex)
            {
                log("Caught Exception {0}, message='{1}'", ex.ToString(), ex.Message);
                topicsCheckListBox.Enabled = true;
            }
        }

        void firehose()
        {
            System.Random r = new System.Random();

            try
            {
                while (!closing)
                {
                    if (firehoseOn)
                    {
                        // publish
                        int index = r.Next(topicsCheckListBox.Items.Count);
                        string topic = topicsCheckListBox.GetItemText(topicsCheckListBox.Items[index]);
                        publish(topic, r.NextDouble().ToString());
                    }
                    System.Threading.Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                log("Caught Exception {0}, message='{1}'", ex.ToString(), ex.Message);
            }
        }

        void startFirehose()
        {
            firehoseOn = true;
            firehoseButton.Text = "Stop &Firehose";
            publishButton.Enabled = false;
        }

        void stopFirehose()
        {
            firehoseOn = false;
            firehoseButton.Text = "Start &Firehose";
            publishButton.Enabled = true;
        }

        private void firehosePublishButton_Click(object sender, EventArgs e)
        {
            if (firehoseOn)
                stopFirehose();
            else
                startFirehose();
        }

        private void subscribeCustomButton_Click(object sender, EventArgs e)
        {
            string topic = customTopicTextBox.Text;
            ushort msgId = client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            pendingSubscribes[msgId] = topic;
        }

        private void unsubscribeCustomButton_Click(object sender, EventArgs e)
        {
            string topic = customTopicTextBox.Text;
            ushort msgId = client.Unsubscribe(new string[] { topic } );
            pendingUnsubscribes[msgId] = topic;
        }
    }
}
