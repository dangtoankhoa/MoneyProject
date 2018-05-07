using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Presentation.Desktop.Seedwork.Localization
{
    /// <summary>
    /// The Translate Markup extension returns a binding to a TranslationData
    /// that provides a translated resource of the specified key
    /// </summary>
    public class TranslateExtension : MarkupExtension
    {
        #region Private Members

        private string _staticKey;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateExtension"/> class.
        /// </summary>
        /// <param name="staticKey">The key.</param>
        public TranslateExtension(string staticKey)
        {
            _staticKey = staticKey;
        }

        public TranslateExtension()
        {

        }
        #endregion

        [ConstructorArgument("staticKey")]
        public string StaticKey
        {
            get { return _staticKey; }
            set { _staticKey = value; }
        }

        private BindingBase _DynamicKey;
        public BindingBase DynamicKey { get; set; }
        //{
        //    get
        //    {
        //        return _DynamicKey;
        //    }
        //    set
        //    {
        //        this._DynamicKey = value;
        //    }
        //} // its necessary to set parameter type as BindingBase to avoid exception that binding can't be used with non DependencyProperty


        public static DependencyProperty DynamicKeyBindingProperty = DependencyProperty.RegisterAttached("DynamicKeyBinding", typeof(object)// set the desired type of Param1 for at least runtime type safety check
            , typeof(TranslateExtension), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnDynamicKeyPropertyChanged)));

        public delegate void OnDynamicKeyChangedDelegate(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static OnDynamicKeyChangedDelegate OnDynamicKeyChanged;
        private static void OnDynamicKeyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (OnDynamicKeyChanged != null)
            {
                OnDynamicKeyChanged(d, e);
            }
        }

        /// <summary>
        /// See <see cref="MarkupExtension.ProvideValue" />
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            DependencyObject targetObject = null;
            DependencyProperty targetProperty;

            if (target != null && target.TargetObject is DependencyObject && target.TargetProperty is DependencyProperty)
            {
                targetObject = (DependencyObject)target.TargetObject;
                targetProperty = (DependencyProperty)target.TargetProperty;
            }
            else
            {
                return this;
            }

            Binding binding = new Binding("Value");

            if (this.DynamicKey != null && targetObject != null)
            {
                BindingOperations.SetBinding(targetObject, TranslateExtension.DynamicKeyBindingProperty, this.DynamicKey);
                binding.Source = new DynamicTranslationData(_staticKey, targetObject);
            }
            else
            {
                binding.Source = new TranslationData(_staticKey);
            }

            return binding.ProvideValue(serviceProvider);
        }
    }
}
