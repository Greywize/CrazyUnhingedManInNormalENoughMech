using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace RPGelements
{
    public abstract class CharStats : ScriptableObject
    {
        #region CONSTRUCTOR
        protected CharStats()
        {

        }
        #endregion

        #region PROTECTED MEMBERS
        protected string playerName { get; set; }
        protected int score { get; set; }
        protected int maxHealth { get; set; }
        protected int currHealth { get; set; }
        protected int ammunition { get; set; }

        #endregion

        #region FUNCTIONS
        private void OnEnable()
        {
            
        }

        private void Awake()
        {
            
        }

        public void decAmmo()
        {
            ammunition--;
        }

        public void decAmmo(int i)
        {
            ammunition -= i;
        }

        public void incAmmo()
        {
            ammunition++;
        }

        public void incAmmo(int i)
        {
            ammunition += i;
        }

        #endregion

    }

}
