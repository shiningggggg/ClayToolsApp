using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExercise.Base
{
    public class SqlExerciseItem
    {
        #region 私有成员
        private string _name = string.Empty;
        private string _title = string.Empty;
        private string _description = string.Empty;
        private bool _changed = false;
        #endregion

        #region 构造函数

        public SqlExerciseItem(string name, string title, string description)
        {
            this._name = name;
            this._title = title;
            this._description = description;
        }
        #endregion

        #region 公共属性
        public bool Changed
        {
            get
            {
                return this._changed;
            }
            set
            {
                this._changed = value;
            }
        }
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name.Equals(value ?? string.Empty) == false)
                {
                    this._name = value ?? string.Empty;
                    this._changed = true;
                }
            }
        }
        public string Title
        {
            get
            {
                return string.IsNullOrEmpty(this._title) ? this._name : this._title;
            }
        }
        public string Description
        {
            get
            {
                return this._description;
            }
        }
        #endregion
    }
}
