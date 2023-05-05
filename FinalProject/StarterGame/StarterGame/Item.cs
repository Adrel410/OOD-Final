using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld {
    
    public class Item : IItem {

        private float _weight;
        private string _name;
        private float _value;
        private IItem _decorator;
        //private IItem _next;
        private bool _isDecorator;

        public Item(string name, float weight) : this(name, weight, 1f)
        {

        }


        public Item() : this("NoName") {

        }

        public Item(string name) : this(name, 1f) {
        }

        public Item(string name, float weight, float value): this(name, weight, value, false) { }

        public Item(string name, float weight, float value, bool isDecorator) {
            _name = name;
            _weight = weight;
            _value = value;
            _decorator = null;
            //_next = null;
            _isDecorator = isDecorator;
        }



        public float Weight { get { return _weight + (_decorator!=null?_decorator.Weight:0); } }

        public float Value { get { return _value + (_decorator != null?_decorator.Value : 0); } }
        public bool IsDecorator { get { return _isDecorator; } }

        public string Name { get { return _name; } }

        public string LongName
        {
            get
            {
                return Name + (_decorator != null ? " ," + _decorator.LongName : "");
            }
        }

        public void AddDecorator(IItem decorator)
        {
            if (decorator.IsDecorator)
            {
                if (_decorator == null)
                {
                    _decorator =  decorator;
                }
                else
                {
                    _decorator.AddDecorator(decorator);
                }
            }
            
        }

        public string Description { get { return LongName + "  weight=" + Weight +", value=" +Value ; } }//or name

    }
    public class ItemContainer : IItemContainer
    {
        private float _weight;
        private string _name;
        private float _value;
        private IItem _decorator;
        //private IItem _next;
        private bool _isDecorator;
        private Dictionary<string, IItem> _items;

        public ItemContainer(string name, float weight) : this(name, weight, 1f)
        {

        }


        public ItemContainer() : this("NoName")
        {

        }

        public ItemContainer(string name) : this(name, 0f)
        {
        }

        public ItemContainer(string name, float weight, float value) 
        {
            _name = name;
            _weight = weight;
            _value = value;
            _decorator = null;
            //_next = null;
            _items = new Dictionary<string, IItem>();
        }


        public float Weight 
        { 
            get 
            {
                float itemsWeight = 0;
                foreach(IItem item in _items.Values)
                {
                    itemsWeight += item.Weight;
                }
                return _weight + itemsWeight; 
            } 
        }

        public float Value 
        {       
            get
            {
                float itemsValues = 0;
                foreach(IItem item in _items.Values)
                {
                    itemsValues += item.Value;
                }
                return _value + itemsValues; 
            } 
        }
        public bool IsDecorator { get { return _isDecorator; } }

        public string Name { get { return _name; } }

        public string LongName
        {
            get
            {
                string longName = Name + "\n";
                foreach (IItem item in _items.Values)
                {
                    longName += item.Description + "\n";
                }
                return longName;
            }
        }
        public string Description { get { return LongName + " : weight = " + Weight + ", value = " + Value; } }//or name



        public void AddDecorator(IItem decorator)
        {
        }
        public void Insert(IItem item)
        {
            _items[item.Name] = item;
        }
        public IItem Remove(string itemName)
        {
            _items.Remove(itemName, out IItem itemToReturn);
            return itemToReturn;


        }
    }
}
