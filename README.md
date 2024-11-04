This is a Auto Scroll for your Scrollviews when you change the focus of the item and is not visible, you can usa awsd or the new system input


This code is based on the video of Des CSK, you can follow all the steps in his video the only thing that is diferrent will be the script ScrollViewAutoScroll
https://www.youtube.com/watch?v=l2_rHUffkJw

![Rogues-Kisses-Deadpool-Comic](https://github.com/user-attachments/assets/73ff3ea4-8e95-40e5-85bc-460b5b780f28)



Steps to use it.:


1- Create a canva

2- Create a Scrollview UI (Please be sure the vales of height and width be positives or it can create a strange behavor

3- On the child of the scrollview -> Viewport -> Content  Add a new component Vertical Layout Group (Checkout child force expand checkboxes) and a add Content Size Filter as a new component too (Vertical fit = preferred size  / horizontal fit = Unconstrained)
![image](https://github.com/user-attachments/assets/7f8c1e1e-248d-4cc8-8fe1-7cfc5ea4b3da)


4- create a Button prefab add in the prefab the script saveItem and drag the text into the inspector

4.1- On navigation use Explicit as you can see in the image below.


![GNB7XgOWcAAZ2W1](https://github.com/user-attachments/assets/39169fb8-357c-4fb4-9cc0-7956c06e963f)


5- Add the script ScrollViewSample and drag and drop into the inspector the child content of the scrollview and the prefab u created (Modify test button Count on the inspector to the number of buttons u want to create)

6- Add the script ScrollViiewAutoScrolll and drag and drop into the inspector the child content and Viewport

![main-qimg-c1f0e96d42d9e2d0881266](https://github.com/user-attachments/assets/037ea597-e5ed-4e8c-857a-d9ca5e253c33)


Now you have a scrollview that will do an autoscroll when you focus one of the prefabs that is not in the mask or the view of the scrollview it works with the system input it means you can move with awsd and using a game controller.



****** How to Focus first element when you activate the menu *************


You can use the script Tienda if you want to press an input when you enter on a trigger for example when you talk to a character and it opens the Menu but if you do not need all that script 
you can call this line where you need it on your script it will focus the first button on your scroll be sure your scroll is active before you call it 

![image](https://github.com/user-attachments/assets/8032f3be-0eef-4193-8fcf-c60d126f05f5)

Note- Be careful to use the correct Scrip depends if you are using Vertical o Grid Layout Group 
![image](https://github.com/user-attachments/assets/f7e50d56-9e95-4ae7-b572-391c760a92c5)




******If you want to to use a Grid Layout Group do the same steps but you will be using the scripts on the folder GridLayout *******

ScrollViewAutoScrollGridLayout will replace ScrollViiewAutoScrolll
ScrollViewBuy will replace ScrollViewSample
You can use a List<string> or use mine List<ItemTienda> but you will need the script ItemTienda you can do that change on line 30 in ScrollViewAutoScrollGridLayout


To use this scripts you need to do something different on the step 3.
3- Now you have o Add a new component Grid Layout Group and a add Content Size Filter as a new component too (Vertical fit = preferred size  / horizontal fit = Unconstrained)
![image](https://github.com/user-attachments/assets/168177bb-7bcc-421c-800a-302f3cf58356)

Note- yoy have to take care you prefab size and modify cell size on the frid layout group to fit your prefab or it will look horrible.

Follow all the steps for Vertical Layout Group and you will have now and autroscroll for GridLayoutgroup
