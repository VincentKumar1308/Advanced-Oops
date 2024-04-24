using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ListDs
{
    public partial class CustomizedList<Type>:IEnumerable,IEnumerator
    {
        int position;
        public IEnumerator GetEnumerator(){
            position=-1;
            return (IEnumerator)this;
        }
        public bool MoveNext(){
            
            if(position<_count-1){
                position++;
                return true;
            }
            Reset();
            return false;

        }
        public void Reset(){
            position=-1;
        }
        public object Current{get{return _array[position];}}
    }
}
