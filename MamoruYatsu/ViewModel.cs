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
            this.Field.StartedGame += (_, __) =>
            {
                this.IsPlaying = true;
                this.IsSelectingParts = false;
                this.IsSelectingStage = false;
            };
            this.Field.EndedGame += (_, __) =>
            {
                this.IsSelectingParts = true;
                this.IsPlaying = false;
            };
            this.Field.PropertyChanged += (_, e) =>
            {
                switch (e.PropertyName)
                {
                    case "Cleared":
                        this.SelectStageCommand.RaiseCanExecuteChanged();
                        break;
                }
            };
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
                    this.RaisePropertyChanged();
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
                    this.RaisePropertyChanged();
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

        private bool isPlaying;
        public bool IsPlaying
        {
            get
            {
                return this.isPlaying;
            }
            private set
            {
                if (this.isPlaying != value)
                {
                    this.isPlaying = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}
