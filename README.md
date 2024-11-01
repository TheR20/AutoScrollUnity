This code is based on the video of Des CSK, you can follow all the steps in the video the only thing tha is diferrent will be the script ScrollViewAutoScroll
https://www.youtube.com/watch?v=l2_rHUffkJw

![Rogues-Kisses-Deadpool-Comic](https://github.com/user-attachments/assets/73ff3ea4-8e95-40e5-85bc-460b5b780f28)



Steps to use it.:
1- Create a canva
2- Create a Scrollview UI (Please be sure the vales of height and width be positives or it can create a strange behavor
3- On the child of the scrollview -> Viewport -> Content  Add a new component Vertical Layout Group (Checkout child force expand checkboxes) and a add Content Size Filter as a new component too (Vertical fit = preferred size  / horizontal fit = Unconstrained)
4- create a Button prefab add in the prefab the script saveItem and drag the text into the inspector 
![GNB7XgOWcAAZ2W1](https://github.com/user-attachments/assets/39169fb8-357c-4fb4-9cc0-7956c06e963f)

5- Add the script ScrollViewSample and drag and drop into the inspector the child content of the scrollview and the prefab u created (Modify test button Count on the inspector to the number of buttons u want to create)
6- Add the script ScrollViiewAutoScrolll and drag and drop into the inspector the child content and Viewport
![main-qimg-c1f0e96d42d9e2d0881266](https://github.com/user-attachments/assets/037ea597-e5ed-4e8c-857a-d9ca5e253c33)


Now you have a scrollview that will to an autoscroll when you focus one of the prefabs that is not in the mask or the view of the scrollview it works with the system input it means you can move with awsd and using a game controller.
