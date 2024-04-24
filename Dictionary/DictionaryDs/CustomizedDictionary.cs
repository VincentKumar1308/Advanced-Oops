using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DictionaryDs
{
   public partial class CustomizedDictionary<Tkey,Tvalue>{
    private int _count=0;
    public int Count{get{return _count;}}
    private int _capacity{get;set;}
    private KeyValue<Tkey,Tvalue> [] _array;

    //constructors
    public  CustomizedDictionary(){
        _count=0;
        _capacity=4;
        _array=new KeyValue<Tkey, Tvalue>[_capacity];
    }

    public  CustomizedDictionary(int size){
        _count=0;
        _capacity=size;
        _array=new KeyValue<Tkey, Tvalue>[_capacity];
    }

    //indexers
   public Tvalue this[Tkey key]{
    get{
       
        Tvalue value=IsPresent(key);
       return value;
    }
    set{
        int position=LinearSearch(key);
        if(position>-1){
            _array[position].Value=value;
        }
    }
   }

    //adding the elements to the Dictionary
    public void Add(Tkey key,Tvalue value){
        if(_count==_capacity){
            GrowSize();
        }
        int position=LinearSearch(key);
        if(position==-1){
        KeyValue<Tkey,Tvalue> name=new KeyValue<Tkey, Tvalue>();
        name.Key=key;
        name.Value=value;
        _array[_count]=name;
        _count++;
    }
    }

    //increasing the size of  the dictionary
    public void GrowSize(){
        _capacity=_capacity*2;
        KeyValue<Tkey,Tvalue>[] temp=new KeyValue<Tkey, Tvalue>[_capacity];
        for(int i=0;i<Count;i++){
            temp[i]=_array[i];
        }
        _array=temp;
    }
    public int LinearSearch(Tkey key){
        int position=-1;
       for(int i=0;i<Count;i++){
        if(_array[i].Key.Equals(key)){
            position=i;
           
        }
       }
        return position;
    }

     public Tvalue IsPresent(Tkey key){
        Tvalue value=default(Tvalue);
       for(int i=0;i<Count;i++){
        if(_array[i].Key.Equals(key)){
           value=_array[i].Value;
          break;
        }
       }
       return value;
       
    }
   }
}