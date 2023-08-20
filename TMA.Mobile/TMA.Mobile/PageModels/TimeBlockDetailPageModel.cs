using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Mobile.PageModels
{
    public class TimeBlockDetailPageModel : FreshBasePageModel
    {
        private string _test = "Hallo het is gelukt, je zit op de detailPagina";

        public string Test
        {
            get { return _test; }
            set
            {
                _test = value;
                RaisePropertyChanged();
            }
        }

    }
}
