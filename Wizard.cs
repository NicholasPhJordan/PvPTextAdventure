using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Wizard : Character
    {
        private float _mana;

        //calls the deafault constructor for the wizard, and then calls the base classes constructor
        public Wizard() : base()
        {
            _mana = 100.0f;
        }
        public Wizard(float healthVal, string nameVal, float damageVal, float manaVal) 
            : base(healthVal, nameVal, damageVal)
        {
            _mana = manaVal;
        }

        public override float Attack(Character enemy)
        {
            if(_mana >= 4)
            {
                float totalDamge = _damage + _mana * .25f;
                _mana -= _mana * .25f;
                //_mana = _mana - (_mana * .25f);
                enemy.TakeDamage(totalDamge);
                return;
            }
            base.Attack(enemy);
        }
    }
}
