using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    public class NPC 
    {
        private Room _currentRoom = null;
        Random rand = new Random();
        private float _power;
        private string _name;
        private float _value;
        //private Dictionary<string, NPC> _npc;

        public string Name { get => _name; set => _name = value; }
        public float Power { get => _power; set => _power = value; }
        public float Value { get => _value; set => _value = value; }
        public NPC(Room room)
        {
            _currentRoom = room;
            //Power = 50;
            //Value = rand.Next(0, 10);

        }
        public NPC(string name, float weight) : this(name, weight, 1f)
        {

        }
        public NPC(string name) : this(name, 1f)
        {
        }
        //public NPC(string name, float power, float value) : this(name, power, value) { }
        public NPC(string name, float power, float value)
        {
            _name = name;
            _power = power;
            _value = value;
            //_npc = new Dictionary<string, NPC>();

        }
        //public void Add(NPC NPC)
        //{
        //    _npc[NPC.Name] = NPC;
        //}
        //public IINPC Remove(string NPCName)
        //{
        //    _npc.Remove(NPCName, out INPC nPCToReturn);
        //    return (IINPC)nPCToReturn;


        //}
        //public string Description { get { return LongName + "  weight=" + Weight + ", value=" + Value; } }
        public static void Combat(bool random, string name, int power, int health)
        {

        }
    }
}
