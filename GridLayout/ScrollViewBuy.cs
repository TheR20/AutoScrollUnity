using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollViewBuy : MonoBehaviour
{
[SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _prefabListItem;

    [Space(10)]
    [Header( "Scroll View Events")]
    [SerializeField] private ItemButtonEvent _eventItemClicked;
    [SerializeField] private ItemButtonEvent _eventItemOnSelect;
    [SerializeField] private ItemButtonEvent _eventItemOnsumbit;
    [SerializeField] private GameObject _objectToDisable;

    [Space(10)]
    [Header("Default Selected Index")]
    [SerializeField] private int _defaultSelectedIndex = 0;

    [Space(10)]
    [Header("For Testing Onlye")]
    [SerializeField] private int _testButtonCount = 1 ;
     private GameObject _firstButton;

     public List<ItemTienda> Items;
    void Start()
    {
       /* if(_testButtonCount > 0){
            TestCreateItem(_testButtonCount);
            UpdateAllButtonNavigatinalReferences();
        }

        StartCoroutine(DelayedSelectChild(_defaultSelectedIndex));*/


     TestCreateItem(1);
    UpdateAllButtonNavigatinalReferences();
    StartCoroutine(DelayedSelectChild(_defaultSelectedIndex));
    }

    public void selectChild (int index)
    {
        int childCount = _content.transform.childCount;

        if(index >= childCount)
        {
            return;
        }

        GameObject chilObject = _content.transform.GetChild(index).gameObject;
        SaveItem item = chilObject.GetComponent<SaveItem>();
        item.ObtainSelectionFocus(); //revisar porque decia ObtainSelectionFocus
    }

    public IEnumerator DelayedSelectChild(int index)
    {
        yield return new WaitForSeconds(1f);
        selectChild(index);
    }

    private void UpdateAllButtonNavigatinalReferences()
    {
       SaveItem[] children = _content.transform.GetComponentsInChildren<SaveItem>(true);
       if(children.Length <2)
       {
        return;
       }
        SaveItem item;
        Navigation navigation;

        for(int i = 0; i < children.Length; i++)
        {
            item = children[i];
            navigation = item.GetComponent<Button>().navigation;

            navigation.selectOnUp = GetNavigationUp(i , children.Length);
            navigation.selectOnDown = GetNavigationDown(i, children.Length);

            item.gameObject.GetComponent<Button>().navigation = navigation;

        }

    }

    // Método para seleccionar el primer botón
    public void SelectFirstButton()
    {
        if (_firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // Limpiar selección previa
            EventSystem.current.SetSelectedGameObject(_firstButton); // Seleccionar el primer botón
            Debug.Log("Si hay un first button");
        }
        else
        {
            Debug.Log("NO hay un first button");
        }
    }

    private Selectable GetNavigationDown(int indexCurrent, int totalEntries)
    {
        SaveItem item;
         if(indexCurrent == totalEntries - 1){
            item =_content.transform.GetChild(0).GetComponent<SaveItem>();
         }
         else
         {
            item = _content.transform.GetChild(indexCurrent +1).GetComponent<SaveItem>();
         }
         return item.GetComponent<Selectable>();
    }

    private Selectable GetNavigationUp(int indexCurrent, int totalEntries)
    {
        SaveItem item;
        if(indexCurrent == 0){
            item = _content.transform.GetChild(totalEntries -1).GetComponent<SaveItem>();
        }
        else
        {
            item = _content.transform.GetChild(indexCurrent -1 ).GetComponent<SaveItem>();
        }
        return item.GetComponent<Selectable>();
    }

    private void TestCreateItem(int count)
    {
         // Crear el botón al inicio
     _firstButton = CreateItem("CLOSE " + "", "","","",true).gameObject;


    foreach(var item in Items)
    {
        string nombre = item.nombreItem;
        string Precio = "Y" + item.Precio;
        string Cantidad = "X" + item.Cantidad;
        CreateItem(nombre ,Precio,Cantidad,"",false);
    }

    CreateItem("CLOSE " + "", "","","",true);

    }

    private SaveItem CreateItem(string name,string fecha,string fechaJuego, string profileId, bool objeto)
    {
        GameObject gObj;
        SaveItem item;

        gObj = Instantiate( _prefabListItem, Vector3.zero, quaternion.identity );
        gObj.transform.SetParent(_content.transform);
        gObj.transform.localScale = new Vector3( 1f, 1f, 1f);
        gObj.transform.localPosition = new Vector3();
        gObj.transform.localRotation = Quaternion.Euler(new Vector3());
        gObj.name = name;

        //set the appropieta param
        item  = gObj.GetComponent<SaveItem>();
        item.ItemNameValue = name;
        item.ItemDateLifeValue = fecha;
        item.ItemDateGameValue = fechaJuego;
        item.ProfileID = profileId;  // Asigna el ID del perfil
         // Asigna el evento OnClick
         if(!objeto)
         item.SetOnClickListener(() => HandleEventItemOnClick(item));
         else
         item.SetOnClickListener(() => Closedelmenus());

        item.OnSlectEvent.AddListener((SaveItem) => {HandleEventOnSelect(item);});
      //  item.OnClickEvent.AddListener((SaveItem) => {HandleEventItemOnClick(item);});
        item.OnSubmitEvent.AddListener((SaveItem) => {HandleEventItemOnSumbit(item);});

        return item;

    }


    private void Closedelmenus()
    {
        _objectToDisable.SetActive(false);
        Time.timeScale = 1f;
    }

    private void HandleEventItemOnSumbit(SaveItem item)
    {
        _eventItemOnsumbit.Invoke(item);
    }

private void HandleEventItemOnClick(SaveItem item)
{
// Cambia el perfil seleccionado al que corresponde al ID del botón clicado
    DataPersistenceManager.instance.ChangeSelectedProfileId(item.ProfileID);

    // Carga los datos del perfil seleccionado
    DataPersistenceManager.instance.LoadGame();

    // Obtiene los datos del juego cargado
    var gameData = DataPersistenceManager.instance.GetGameData();

    // Carga la escena correspondiente al perfil cargado
    if (!string.IsNullOrEmpty(gameData.currentScene))
    {
        SceneManager.LoadSceneAsync(gameData.currentScene);
    }
    else
    {
        Debug.LogWarning("No se encontró ninguna escena en los datos del perfil.");
    }

    // Cierra el panel de scroll después de cargar
    _objectToDisable.SetActive(false);

    // Invoca el evento de clic del elemento si es necesario
    _eventItemClicked.Invoke(item);
}

public void RefreshContent()
{
    // Limpiar los elementos actuales en `_content`
    foreach (Transform child in _content)
    {
        Destroy(child.gameObject);
    }

    // Volver a crear la lista de elementos
    TestCreateItem(_testButtonCount);  // Puedes pasar el número de botones que necesitas
    UpdateAllButtonNavigatinalReferences();  // Actualizar referencias de navegación
}

private void HandleEventOnSelect(SaveItem item)
{
    ScrollViewAutoScrollGridLayout scrollViewAutoScroll = GetComponent<ScrollViewAutoScrollGridLayout>();
    if (scrollViewAutoScroll != null)
    {
        scrollViewAutoScroll.HandleOnselectChange(item.gameObject);
    }
    _eventItemOnSelect.Invoke(item);
}
}
