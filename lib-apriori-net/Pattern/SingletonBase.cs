using Aspose.Cells;
using lib_apriori_net.Models;
using System.Collections.Generic;

namespace lib_apriori_net.Pattern
{
    public class SingletonBase
    {

        #region Singleton
        private static SingletonBase _instance;

        public static SingletonBase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SingletonBase();
                }
                return _instance;
            }
        }
        #endregion


        public List<ResultProcess> FinalResult = new List<ResultProcess>();

    }
}
