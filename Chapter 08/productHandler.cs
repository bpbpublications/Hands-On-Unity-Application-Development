using System.Collections.Generic;
using UnityEngine;
public class productHandler : MonoBehaviour
{
  //Variable to store the list of product models
  List<GameObject> products = new List<GameObject>();
  //Stores the index of currently active product
  private int currentProduct = 0;
  // Start is called before the first frame update
  void Start()
  {
    //Iterating through all the children(product models)
    for (int i = 0; i < this.transform.childCount; i++)
    {
      //Adding the product models to the list
      products.Add(this.transform.GetChild(i).gameObject);
      //Initially except the first child all the rest should be inactive 
      products[i].SetActive(i == 0 ? true : false);
    }
  }
  // Update is called once per frame
  void Update()
  {
    //Return if there are no product models
    if (products.Count <= 0)
      return;
    //On Pressing the Left Arrow Key
    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      //Select the Previous Product
      currentProduct--;
      //Allows the products to be selected in a carousel format
      currentProduct = currentProduct < 0 ? products.Count - 1 : currentProduct;
      updateCurrentProduct(currentProduct);
    }

    //On Pressing the Right Arrow Key
    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
       //Select the Next Product
       currentProduct++;
       //Allows the products to be selected in a carousel format
       currentProduct = currentProduct >= products.Count ? 0 : currentProduct;
       updateCurrentProduct(currentProduct);
    }
  }
  void updateCurrentProduct(int no)
  {
    //Except the currentProduct all the rest children should be inactive 
    for (int i = 0; i < this.transform.childCount; i++)
      products[i].SetActive(i == no ? true : false);
  }
}
