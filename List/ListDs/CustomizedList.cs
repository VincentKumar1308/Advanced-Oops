using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListDs
{
    public partial class CustomizedList<Type>//creation of the generic type class
    {
        //declaration of index and length of the list
        private int _count=0;
        private int _capacity;

        //declaration of the array
        private Type[] _array;

        //returning  count
        public int Count{
            get{return _count;}
        }



        //Constructors

        //creation of the constructors
        public  CustomizedList(){
            _count=0;
            _capacity=4;
            _array=new Type[_capacity];

        }
        //parameterized constructors
        public CustomizedList(int size){
            _capacity=size;
          _array=new Type[_capacity];
          
        }

        //indexers
        public Type this[int index]
        {
            get{return _array[index];}
            set{_array[index]=value;}
        }


        //methods

        //creating methods for adding elements in the list
        public void AddingElements(Type elements){
            if(_count==_capacity){
                 GrowSize();
            }
            _array[_count]=elements;
            _count++;
        }
        
        //building  size of the list automatically
            public void GrowSize(){
            _capacity=_count+_capacity;
            Type[] temp=new Type[_capacity];
            for(int i=0;i<Count;i++){
                temp[i]=_array[i];
            }
            _array=temp;
        }

        //creating methods for the Adding two lists 
        public void AddingLists(CustomizedList<Type> elements){
            _capacity=elements.Count+_count;
            Type[] temp=new Type[_capacity];
            for(int i=0;i<_count;i++){
                temp[i]=_array[i];
            }
            int k=0;
            for(int i=_count;i<_capacity;i++){
                temp[i]=elements[k];
                k++;
            }
            _array=temp;
            _count=_count+elements.Count;
        }

        //contains
        public bool Contains(Type element){
            bool isConatins=false;
            foreach(Type word in _array){
                if(word.Equals(element)){
                    isConatins=true;
                }
            }
            return isConatins;
        }

        //index of
        public  int  IndexOf(Type element){
            int index=-1;
            for(int i=0;i<Count;i++){
                if(element.Equals(_array[i])){
                    index=i;
                    break;
                }
            }
            return index;
        }

        //inserting an element
        public void Insert(Type element,int position){
            Type[] temp=new Type[Count+1];
            for(int  i=0;i<Count+1;i++){
                if(i<position){
                    temp[i]=_array[i];
                }
                else if(i==position){
                    temp[i]=element;
                }
                else{
                    temp[i]=_array[i-1];
                }
            }
            _array=temp;
            _count++;

        }

        //removing element using the index
        public void  RemoveAt(int position){
            
            for(int i=0;i<Count-1;i++){
                if(i>=position){
                    _array[i-1]=_array[i];
                }
            }
            _count--;
        }

        //Removing the element using the value
        public  bool Remove(Type element){
            IndexOf(element);
            int position=IndexOf(element);

            if(position>=0){
                RemoveAt(position);
                return true;
            }
            else{
                return false;
            }

        }

        //Reverse of the  list
        public void Reverse(){
            Type[] temp=new Type[_capacity];
            for(int i=_count-1,j=0;i>=0;i--,j++){
                temp[j]=_array[i];
            }
            _array=temp;
        }

        //Inserting list inside list of particular position
        public void InsertRange(CustomizedList<Type> elements,int position){
            _capacity=_count+elements.Count;
            Type[] temp=new Type[_capacity];
            for(int i=0;i<position;i++){
                temp[i]=_array[i];
            }
            int k=0;
            for(int i=position;i<position+elements.Count;i++){
                temp[i]=elements[k];
                k++;
            }
            k=position;
            for(int i=position+elements.Count;i<_count+elements.Count;i++){
                temp[i]=_array[k];
                k++;
            }
            _array=temp;
            _count=_count+elements.Count;
        }

        //array sorting
        public void Sorting(){
            for(int i=0;i<Count;i++){
                for(int  j=i;j<Count;j++){
                    if(IsGreater(_array[i],_array[j])){
                        Type Temp=_array[i];
                        _array[i]=_array[j];
                        _array[j]=Temp;
                    }
                }
            }
        }
        public bool  IsGreater(Type array1,Type array2){
          int result=Comparer<Type>.Default.Compare(array1,array2);
          if(result>0){
            return true;
          }
          return  false;
        }

        }
}