namespace lib_apriori_net.Models
{
    public class ResultProcess
    {
        public string Combination { get; set; }
        public string Remaining { get; set; }
        public string Confidence { get; set; }

        public string Lift { get; set; }

        public ResultProcess(string combination, string remaining, string confidence, string lift)
        {
            Combination = combination;
            Remaining = remaining;
            Confidence = confidence;
            Lift = lift;
        }

    }
}
