using IDataMunging;
using System;
using System.Text;

namespace DataMungingLib
{
    internal class DefaultStringRecord : IStringRecord
    {
        private string[] values;

        public DefaultStringRecord(string[] values)
        {
            this.values = values;
        }

        public int FieldCount
        {
            get
            {
                return values.Length;
            }
        }

        public string GetField(int i)
        {
            if (i < 0 || i >= values.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return values[i];
        }

        public override bool Equals(object obj)
        {
			if (this == obj)
			{
				return true;
			}
			
            if (obj == null || !(obj is DefaultStringRecord))
            {
                return false;
            }

            DefaultStringRecord that = (DefaultStringRecord)obj;

            if (this.FieldCount != that.FieldCount)
            {
                return false;
            }

            for (int i = 0; i < this.FieldCount; i++)
            {
                if (!this.GetField(i).Equals(that.GetField(i)))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (string v in values)
            {
                hash = hash * 23 + v.GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (string v in values)
            {
                if (sb.Length > 1)
                {
                    sb.Append(", ");
                }
                sb.Append(v);
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
