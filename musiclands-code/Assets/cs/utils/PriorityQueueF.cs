﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueueF<T> {
  List<PqElementF<T>> elements;
  bool isMaxHeap;
  
  public PriorityQueueF(){
    elements = new List<PqElementF<T>>();
    isMaxHeap = true;
  }
  public PriorityQueueF(bool isMax){
    this.isMaxHeap = isMax;
    elements = new List<PqElementF<T>>();
  }
    
  public void Enqueue(T element, float priority){
    PqElementF<T> neu = new PqElementF<T>(element, priority);
    
    elements.Add(neu);
    
    // tu imamo dve možnosti:
    // 1. prioriteta ni več večja od trenutnega elementa
    // 2. gledamo zadnji element
    
    Sink(elements.Count - 1);
  }
  
  public bool IsEmpty(){
    return elements.Count == 0;
  }
  
  public T Dequeue(){
    if(this.elements.Count == 0)
      return default(T);
    
    T gib = this.elements[0].element;
    this.elements[0] = this.elements[this.elements.Count - 1];
    this.elements.RemoveAt(this.elements.Count - 1);
    this.Sink(0);
    return gib;
  }
  
  
  // internal methods
  
  void Swap(int index_a, int index_b){
    PqElementF<T> tmp = this.elements[index_a];
    this.elements[index_a] = this.elements[index_b];
    this.elements[index_b] = tmp;
  }
  
  void Sink(int indexSink){
    if(this.isMaxHeap)
      SinkMax(indexSink);
    else
      SinkMin(indexSink);
  }
  
  void SinkMax(int indexSink){
    /** heap - kopica (v tabeli)
     * i    - index elementa, ki ga pogrezamo
     * d    - velikost tabele?
     * asce - sort order
     */
    
    int len = this.elements.Count;
    
    if( ( (indexSink << 1) + 1) >= len)
      return;             //smo na dnu drevesa
      
    int indexBase, ivec, imanj;
    indexBase = (indexSink << 1) + 1;
    
    if( indexBase + 1 >= len ){
      if( elements[indexSink].priority < elements[indexBase].priority ) {
            Swap(indexSink, indexBase); 
        }
        // če imamo samo enega otroka, to pomeni, da bo naslednji otrok na dnu drevesa.
        return;
    }
    
    if( elements[indexBase].priority >= elements[indexBase + 1].priority ) {
          ivec = indexBase;
          imanj = indexBase + 1;
    }
    else{
      ivec = indexBase + 1;
      imanj = indexBase;
    }
    
    
    if( elements[indexSink].priority < elements[ivec].priority ) {
          Swap(indexSink,ivec);
          SinkMax(ivec);
          return;
    }    
    if( elements[indexSink].priority < elements[imanj].priority ) {    
          Swap(indexSink,imanj);
          SinkMax(imanj);
    }
  }
  
  void SinkMin(int indexSink){
    int len = this.elements.Count;
    
    if( ( (indexSink << 1) + 1) >= len)
      return;             //smo na dnu drevesa
      
    int indexBase, ivec, imanj;
    indexBase = (indexSink << 1) + 1;
    
    if( indexBase + 1 >= len ){
      if( elements[indexSink].priority > elements[indexBase].priority ) {
        Swap(indexSink, indexBase); 
      }
      // če imamo samo enega otroka, to pomeni, da bo naslednji otrok na dnu drevesa.
      return;
    }
    
    if( elements[indexBase].priority <= elements[indexBase + 1].priority) {
      ivec = indexBase;
      imanj = indexBase + 1;
    }
    else{
      ivec = indexBase + 1;
      imanj = indexBase;
    }
    
    
    if( elements[indexSink].priority > elements[ivec].priority ) {
      Swap(indexSink,ivec);
      SinkMin(ivec);
      return;
    }
    if( elements[indexSink].priority > elements[imanj].priority ) {    
      Swap(indexSink,imanj);
      SinkMin(imanj);
    }
  }
  
}

class PqElementF<T> {
  public float priority;
  public T element;
  public PqElementF<T> next;
  
  public PqElementF(T element, float priority){
    this.element = element;
    this.priority = priority;
    this.next = null;
  }
}
