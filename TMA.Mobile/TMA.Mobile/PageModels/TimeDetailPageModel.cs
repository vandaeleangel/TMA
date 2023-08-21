using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class TimeDetailPageModel : FreshBasePageModel
    {
        private ITimeBlockService _timeBlockService;

        private TimeBlock _timeBlock;

        public TimeBlock TimeBlock
        {
            get { return _timeBlock; }
            set
            {
                _timeBlock = value;
                RaisePropertyChanged();
            }
        }

        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public TimeDetailPageModel(ITimeBlockService timeBlockService)
        {
            _timeBlockService = timeBlockService;
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
            DeleteCommand = new Command(Delete);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            TimeBlock = initData as TimeBlock;
        }
        private void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        private async void Cancel(object obj)
        {
            await CoreMethods.PopPageModel(modal: true);
        }

        private void Save(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
