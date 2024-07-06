using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Consumer.Console
{
    public class ParametersModel
    {

        public ParametersModel() {
            BootstrapServer = "localhost:9092";
            TopicName = "pramos-topic";
            GroupId = "Group 1";
        }

        public string BootstrapServer { get; set; }
        public string TopicName { get; set; }
        public string GroupId { get; set; }
    }
}
