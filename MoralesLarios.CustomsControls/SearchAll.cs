using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using MoralesLarios.CustomsControls.HelpControls;
using System.ComponentModel;
using MoralesLarios.CustomsControls.MLTaskPool;
using MoralesLarios.CustomsControls.Helpers;
using old = System.Drawing;
using MoralesLarios.CustomsControls.HelpControls.EventArgs;

namespace MoralesLarios.CustomsControls
{
    [old.ToolboxBitmap(typeof(SearchAll), "SearchAll.bmp")]
    public class SearchAll : Control
    {
        #region Direct Events

        public event EventHandler<ElementPopulateEventArgs> InsertingSugerentControl;

        protected virtual void OnInsertingSugerentControl(ElementPopulateEventArgs evargs) => InsertingSugerentControl?.Invoke(this, evargs);

        #endregion


        private TextBox    textboxPrincipal;
        private TextBox    textboxSearch;
        private Popup      popup;
        private Button     buttonCancelFilter;
        private Button     buttonFilter;
        private StackPanel filterPanel;
        private Image      imgFilter;

        private ISupportFilteresSearch supportFiltersSearch;
        private ITaskPool              taskPool;
        private IFuncFilterFactory     funcFilterFactory;



        static SearchAll()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchAll), new FrameworkPropertyMetadata(typeof(SearchAll)));

            double width  = System.Windows.SystemParameters.PrimaryScreenWidth;
            double height = System.Windows.SystemParameters.PrimaryScreenHeight;
        }


        #region DependencyProperties

        public static readonly DependencyProperty PopUpWidthProperty =
            DependencyProperty.Register(nameof(PopUpWidth), typeof(double), typeof(SearchAll));

        public double PopUpWidth
        {
            get { return (double)GetValue(PopUpWidthProperty); }
            set { SetValue(PopUpWidthProperty, value); }
        }

        public static readonly DependencyProperty PopUpHeightProperty =
            DependencyProperty.Register(nameof(PopUpHeight), typeof(double), typeof(SearchAll));

        public double PopUpHeight
        {
            get { return (double)GetValue(PopUpHeightProperty); }
            set { SetValue(PopUpHeightProperty, value); }
        }



        private static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(SearchAll), new PropertyMetadata(null, OnTextPropertyChanged));

        private static void OnTextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var _obj = sender as SearchAll;

            if(_obj.textboxPrincipal != null)
            {
                _obj.textboxSearch.Text = e.NewValue?.ToString();
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }



        private static readonly DependencyPropertyKey IsSearchedPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsSearched), typeof(bool), typeof(SearchAll), new PropertyMetadata(false));
        public static readonly DependencyProperty IsSearchedProperty = IsSearchedPropertyKey.DependencyProperty;
        public bool IsSearched
        {
            get { return (bool)GetValue(IsSearchedProperty); }
            protected set { SetValue(IsSearchedPropertyKey, value); }
        }


        private static readonly DependencyPropertyKey IsSearchingPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsSearching), typeof(bool), typeof(SearchAll), new PropertyMetadata(false));
        public static readonly DependencyProperty IsSearchingProperty = IsSearchingPropertyKey.DependencyProperty;

        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingProperty); }
            protected set { SetValue(IsSearchingPropertyKey, value); }
        }


        public static readonly DependencyProperty IsKeySensitiveProperty =
            DependencyProperty.Register(nameof(IsKeySensitive), typeof(bool), typeof(SearchAll), new PropertyMetadata(false));

        public bool IsKeySensitive
        {
            get { return (bool)GetValue(IsKeySensitiveProperty); }
            set { SetValue(IsKeySensitiveProperty, value); }
        }





        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(SearchAll), new PropertyMetadata(null));

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        private static readonly DependencyPropertyKey FilteredItemdSourcePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(FilteredItemdSource), typeof(IEnumerable<object>), typeof(SearchAll), new PropertyMetadata(null));
        public static readonly DependencyProperty FilteredItemdSourceProperty = FilteredItemdSourcePropertyKey.DependencyProperty;
        public IEnumerable<object> FilteredItemdSource
        {
            get { return supportFiltersSearch?.GetFilteredItemSource(ItemsSource); }
            protected set { SetValue(FilteredItemdSourcePropertyKey, value); }
        }



        public static readonly DependencyProperty CloseButtonPopUpProperty =
            DependencyProperty.Register(nameof(CloseButtonPopUp), typeof(Button), typeof(SearchAll), new PropertyMetadata(null));

        public Button CloseButtonPopUp
        {
            get { return (Button)GetValue(CloseButtonPopUpProperty); }
            set { SetValue(CloseButtonPopUpProperty, value); }
        }


        public static readonly DependencyProperty FieldsSugerenciesSearchProperty =
            DependencyProperty.Register(nameof(FieldsSugerenciesSearch), typeof(IEnumerable<string>), typeof(SearchAll),new PropertyMetadata(null));

        public IEnumerable<string> FieldsSugerenciesSearch
        {
            get { return (IEnumerable<string>)GetValue(FieldsSugerenciesSearchProperty); }
            set { SetValue(FieldsSugerenciesSearchProperty, value); }
        }



        public static readonly DependencyProperty FieldsSearchProperty =
            DependencyProperty.Register(nameof(FieldsSearch), typeof(IEnumerable<string>), typeof(SearchAll), new PropertyMetadata(null));

        public IEnumerable<string> FieldsSearch
        {
            get { return (IEnumerable<string>)GetValue(FieldsSearchProperty); }
            set { SetValue(FieldsSearchProperty, value); }
        }


        public static readonly DependencyProperty NumberSugerencyElementsProperty =
            DependencyProperty.Register(nameof(NumberSugerencyElements), typeof(int), typeof(SearchAll), new PropertyMetadata(20));

        public int NumberSugerencyElements
        {
            get { return (int)GetValue(NumberSugerencyElementsProperty); }
            set { SetValue(NumberSugerencyElementsProperty, value); }
        }



        public static readonly DependencyProperty FilterClassProperty =
            DependencyProperty.Register(nameof(FilterClass), typeof(FilterType), typeof(SearchAll), new PropertyMetadata(FilterType.Contains));

        public FilterType FilterClass
        {
            get { return (FilterType)GetValue(FilterClassProperty); }
            set { SetValue(FilterClassProperty, value); }
        }


        public static readonly DependencyProperty CustomFuncActionProperty =
            DependencyProperty.Register(nameof(CustomFuncAction), typeof(Func<object, object, bool>), typeof(SearchAll), new PropertyMetadata(null, OnCustomFuncActionPropertyChanged));

        private static void OnCustomFuncActionPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var _obj = sender as SearchAll;

            if(_obj.funcFilterFactory == null) _obj.funcFilterFactory = new FuncFilterFactory();

            _obj.funcFilterFactory.Custom = (Func<object, object, bool>) e.NewValue;
        }


        public Func<object, object, bool> CustomFuncAction
        {
            get { return (Func<object, object, bool>)GetValue(CustomFuncActionProperty); }
            set { SetValue(CustomFuncActionProperty, value); }
        }





        #endregion


        #region Commands

        protected ICommand CancelFilterCommand
        {
            get
            {
                return new RelayCommand
                (

                    (o) => ResetSearch(),
                    (o) => IsSearched

                );
            }
        }


        protected ICommand FilterCommand
        {
            get
            {
                return new RelayCommand
                (

                    (o) => FilterPrincipal(),
                    (o) => ! string.IsNullOrEmpty(Text)

                );
            }
        }





        #endregion


        #region RoutedCommands

        public static readonly RoutedEvent FilteredEvent =
            EventManager.RegisterRoutedEvent(nameof(Filtered), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchAll));


        public event RoutedEventHandler Filtered
        {
            add    { AddHandler(FilteredEvent   , value); }
            remove { RemoveHandler(FilteredEvent, value); }
        }

        protected virtual void RaiseFilteredEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(SearchAll.FilteredEvent);
            RaiseEvent(args);
        }



        public static readonly RoutedEvent FilteringEvent =
            EventManager.RegisterRoutedEvent(nameof(Filtering), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchAll));


        public event RoutedEventHandler Filtering
        {
            add    { AddHandler   (FilteringEvent, value); }
            remove { RemoveHandler(FilteringEvent, value); }
        }

        protected virtual void RaiseFilteringEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(SearchAll.FilteringEvent);
            RaiseEvent(args);
        }




        public static readonly RoutedEvent ResetFilteredEvent =
            EventManager.RegisterRoutedEvent(nameof(ResetFiltered), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchAll));


        public event RoutedEventHandler ResetFiltered
        {
            add    { AddHandler   (ResetFilteredEvent, value); }
            remove { RemoveHandler(ResetFilteredEvent, value); }
        }

        protected virtual void ResetRaiseFilteredEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(SearchAll.ResetFilteredEvent);
            RaiseEvent(args);
        }



        public static readonly RoutedEvent ResetFilteringEvent =
            EventManager.RegisterRoutedEvent(nameof(ResetFiltering), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchAll));


        public event RoutedEventHandler ResetFiltering
        {
            add    { AddHandler   (ResetFilteringEvent, value); }
            remove { RemoveHandler(ResetFilteringEvent, value); }
        }

        protected virtual void RaiseResetFilteringEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(SearchAll.ResetFilteringEvent);
            RaiseEvent(args);
        }





        #endregion



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Initializecontrols();

            if(funcFilterFactory == null) funcFilterFactory = new FuncFilterFactory();

            popup.Width  = System.Windows.SystemParameters.PrimaryScreenWidth;
            popup.Height = System.Windows.SystemParameters.PrimaryScreenHeight;

        }

        #region Private Methods

        private void Initializecontrols()
        {
            GetTemplateChields();

            InitializeParentParts();

            if (ItemsSource == null) return;

            InitializePopulateStackPanelControls();

            InitializeTaskPool();

            InitializeGetParentControlsEvents();

        }

        private void GetTemplateChields()
        {
            textboxPrincipal   = GetTemplateChild("PART_Text") as TextBox;
            textboxSearch      = GetTemplateChild("PART_Search") as TextBox;
            popup              = GetTemplateChild("PART_PopUp") as Popup;
            CloseButtonPopUp   = GetTemplateChild("PART_ButtonClosePopPup") as Button;
            buttonCancelFilter = GetTemplateChild("PART_ButtonCancelFilter") as Button;
            buttonFilter       = GetTemplateChild("PART_ButonFilter") as Button;
            filterPanel        = GetTemplateChild("PART_FilterPanel") as StackPanel;
            imgFilter          = GetTemplateChild("PART_ImgFilter") as Image;
        }

        private void InitializeParentParts()
        {
            textboxPrincipal.Text = Text;

            imgFilter.Visibility = Visibility.Hidden;

            buttonCancelFilter.Command = CancelFilterCommand;
            buttonFilter.Command = FilterCommand;
        }

        private void InitializePopulateStackPanelControls()
        {
            Action<string> actionClick = s =>
            {
                textboxPrincipal.Text = s;

                FilterPrincipal();
            };

            IPopulateStackPanelControls populateStackPanelControls = new PopulateStackPanelControls(filterPanel, actionClick);
            populateStackPanelControls.InsertingSugerentControl += PopulateStackPanelControls_InsertingSugerentControl;
            supportFiltersSearch = new SupportFilteresSearch(populateStackPanelControls, ItemsSource);
        }

        private void PopulateStackPanelControls_InsertingSugerentControl(object sender, HelpControls.EventArgs.ElementPopulateEventArgs e)
        {
            OnInsertingSugerentControl(e);

            //if(e.Text?.Contains("Ren") ?? false)
            //{
            //    if (e.ControlAdd != null)
            //    {
            //        var label = new Label();
            //        label.Content = $"Cambiado ----> {e.Text}"; 

            //        e.ControlAdd = label;
                    
            //    }
            //}
        }

        private void InitializeTaskPool()
        {
            taskPool = new TaskPool(new TaskPoolManager());
            taskPool.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsWorking")
                {
                    IsSearching = taskPool.IsWorking;

                    imgFilter.Visibility = IsSearching ? Visibility.Visible : Visibility.Hidden;
                }
            };
        }

        private void InitializeGetParentControlsEvents()
        {
            textboxPrincipal.TextChanged += (sender, e) =>
            {
                //popup.IsOpen = false;
                Text = textboxPrincipal.Text;
                var funcFilter = funcFilterFactory.GetFuncFilter(FilterClass, IsKeySensitive);

                var text = Text;
                var numberSugerencyElements = NumberSugerencyElements;
                var fieldsSugerenciesSearch = FieldsSugerenciesSearch;

                Action action = () => supportFiltersSearch.PopulateSubResults(text, funcFilter, numberSugerencyElements, fieldsSugerenciesSearch);
                taskPool.AddAction(action);
            };

            textboxPrincipal.GotFocus += (sender, e) =>
            {

                popup.IsOpen = true;
                textboxSearch.Focus();


            };

            textboxSearch.KeyDown += (sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    textboxPrincipal.Text = string.Empty;
                    popup.IsOpen = false;
                    IsSearched = false;
                }

                if (e.Key == Key.Return)
                {
                    //popup.IsOpen = false;

                    if (Text == string.Empty)
                    {
                        ResetSearch();
                    }
                    else
                    {
                        FilterPrincipal();
                    }
                }
            };

            CloseButtonPopUp.Click += (sender, e) => popup.IsOpen = false;
        }

        #endregion


        #region Public Methods



        public async void FilterPrincipal(string txtSearch = null)
        {
            if (txtSearch != null) Text = txtSearch;

            RaiseFilteringEvent();

            popup.IsOpen = false;

            var funcFilter = funcFilterFactory.GetFuncFilter(FilterClass, IsKeySensitive);
            await supportFiltersSearch.FilterItemsSourceAsync(Text, funcFilter, FieldsSearch);

            popup.IsOpen = false;
            IsSearched = true;

            RaiseFilteredEvent();
        }


        public void ResetSearch()
        {
            if (IsSearched)
            {
                RaiseResetFilteringEvent();

                Func<object, object, bool> funcFilter = (a, b) => true;
                supportFiltersSearch.FilterItemsSource(Text, funcFilter, FieldsSearch);

                Text = string.Empty;

                popup.IsOpen = false;
                IsSearched = false;

                ResetRaiseFilteredEvent();
            }


        }




        #endregion


    }
}
