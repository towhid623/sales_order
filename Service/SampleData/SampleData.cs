namespace Service.SampleData
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Orders
    {

        private OrdersOrder[] orderField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Order")]
        public OrdersOrder[] Order
        {
            get
            {
                return this.orderField;
            }
            set
            {
                this.orderField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OrdersOrder
    {

        private OrdersOrderWindow[] windowsField;

        private string nameField;

        private string stateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Window", IsNullable = false)]
        public OrdersOrderWindow[] Windows
        {
            get
            {
                return this.windowsField;
            }
            set
            {
                this.windowsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OrdersOrderWindow
    {

        private OrdersOrderWindowSubElement[] subElementsField;

        private string nameField;

        private byte quantityOfWindowsField;

        private byte totalSubElementsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SubElement", IsNullable = false)]
        public OrdersOrderWindowSubElement[] SubElements
        {
            get
            {
                return this.subElementsField;
            }
            set
            {
                this.subElementsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte QuantityOfWindows
        {
            get
            {
                return this.quantityOfWindowsField;
            }
            set
            {
                this.quantityOfWindowsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte TotalSubElements
        {
            get
            {
                return this.totalSubElementsField;
            }
            set
            {
                this.totalSubElementsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class OrdersOrderWindowSubElement
    {

        private byte elementField;

        private string typeField;

        private ushort widthField;

        private ushort heightField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Element
        {
            get
            {
                return this.elementField;
            }
            set
            {
                this.elementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
    }
}
