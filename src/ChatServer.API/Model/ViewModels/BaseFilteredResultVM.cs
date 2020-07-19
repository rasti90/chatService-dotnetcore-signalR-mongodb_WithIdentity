using System;
using System.Collections.Generic;

namespace ChatServer.API.Model.ViewModels {
    public abstract class BaseFilteredResultVM<T> where T: class
    {
        public IEnumerable<T> Result{get;set;}
        public Uri NextUri{get;set;}   
        public Uri PreviousUri{get;set;}

        public BaseFilteredResultVM(){
            
        }
    }
}