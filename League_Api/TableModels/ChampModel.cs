using System;
using System.Collections.Generic;
using System.Text;

namespace League_Api.TableModels
{
    public class ChampModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Style { get; set; }
        public int Difficulty { get; set; }
        public string DamageType { get; set; }
        public int Damage { get; set; }
        public int Sturdiness { get; set; }
        public int CrowdControl { get; set; }
        public int Mobility { get; set; }
        public int Functionality { get; set; }

        public ChampModel() { }

        public ChampModel(int id, string name, string _class, int style, int difficulty, 
            string damageType, int damage, int sturdiness, int crowdControl, int mobility, 
            int functionality)
        {
            this.Id = id;
            this.Name = name;
            this.Class = _class;
            this.Style = style;
            this.Difficulty = difficulty;
            this.DamageType = damageType;
            this.Damage = damage;
            this.Sturdiness = sturdiness;
            this.CrowdControl = crowdControl;
            this.Mobility = mobility;
            this.Functionality = functionality;
        }
    }
}
