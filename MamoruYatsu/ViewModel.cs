using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MamoruYatsu.Stages;

namespace MamoruYatsu
{
    class ViewModel : NotificationObject
    {
        public ViewModel()
        {
            this.Field = new Field();
        }

        public Field Field { get; private set; }

        private DelegateCommand<string> selectStageCommand;
        public DelegateCommand<string> SelectStageCommand
        {
            get
            {
                if (this.selectStageCommand == null)
                    this.selectStageCommand = new DelegateCommand<string>(
                        i =>
                        {
                            IStage stage;
                            switch (i)
                            {
                                case "1":
                                    stage = new Stage1();
                                    break;
                                case "2":
                                    stage = new Stage2();
                                    break;
                                case "3":
                                    stage = new Stage3();
                                    break;
                                default:
                                    throw new ArgumentException();
                            }
                            this.Field.StartGame(stage);
                        },
                        i => this.Field.Cleared + 1 >= int.Parse(i)
                    );
                return this.selectStageCommand;
            }
        }

        private bool isSelectingStage;
        public bool IsSelectingStage
        {
            get
            {
                return this.isSelectingStage;
            }
            private set
            {
                if (this.isSelectingStage != value)
                {
                    this.isSelectingStage = value;
                    this.RaisePropertyChanged(() => this.IsSelectingStage);
                }
            }
        }

        private bool isSelectingParts = true;
        public bool IsSelectingParts
        {
            get
            {
                return this.isSelectingParts;
            }
            private set
            {
                if (this.isSelectingParts != value)
                {
                    this.isSelectingParts = value;
                    this.RaisePropertyChanged(() => this.IsSelectingParts);
                }
            }
        }

        private DelegateCommand goToSelectStageCommand;
        public DelegateCommand GoToSelectStageCommand
        {
            get
            {
                if (this.goToSelectStageCommand == null)
                    this.goToSelectStageCommand = new DelegateCommand(() =>
                    {
                        this.IsSelectingStage = true;
                        this.IsSelectingParts = false;
                    });
                return this.goToSelectStageCommand;
            }
        }

        private DelegateCommand goToSelectPartsCommand;
        public DelegateCommand GoToSelectPartsCommand
        {
            get
            {
                if (this.goToSelectPartsCommand == null)
                    this.goToSelectPartsCommand = new DelegateCommand(() =>
                    {
                        this.IsSelectingParts = true;
                        this.IsSelectingStage = false;
                    });
                return this.goToSelectPartsCommand;
            }
        }
    }
}
