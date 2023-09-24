using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{


    [SerializeField] private GamePlayerSt gamePlayerSt = new GamePlayerSt();
    [SerializeField] private GraficsAndLagunages graficsAndLagunages = new GraficsAndLagunages();
    [SerializeField] private KeysClass keysClass;

    [SerializeField] private GameObject menuKeys;
    [SerializeField] private List<keysCodeOne> keysCodeOne = new List<keysCodeOne>();

    [SerializeField] private SalvarDadosSettings salvarDadosSettings = new SalvarDadosSettings();

    void Start()
    {
        keysClass = new KeysClass(keysCodeOne, menuKeys);
        setMenusActiveOrDesactive(true, false, false);
        LoadData();
    }

    void LoadData()
    {
        if (!File.Exists(Path.Combine(Application.streamingAssetsPath, "config.txt")))
            File.Create(Path.Combine(Application.streamingAssetsPath, "config.txt"));


        gamePlayerSt.LoadData(salvarDadosSettings.LoadDataFirst());
        graficsAndLagunages.LoadData(salvarDadosSettings.LoadData());
        keysClass.LoadData(salvarDadosSettings.LoadData());
    }

    void setMenusActiveOrDesactive(bool SS,bool TT, bool GG)
    {
        gamePlayerSt.SetMenuActiveOrDesactive(SS);
        graficsAndLagunages.SetMenuActiveOrDesactive(GG);
        keysClass.SetMenuActiveOrDesactive(TT);
    }


    public void OnKeyCharger(GameObject newCurrectKey)
    {
        keysClass.ChargeKey(newCurrectKey);
    }

    public void setNewGrafics()
    {
        graficsAndLagunages.SetnewGrafics();
    }

    public void SetResolution()
    {
        graficsAndLagunages.SetNewResolution();
    }
    
    public void SetLanguagns()
    {
        graficsAndLagunages.SetIdioma();
    }

    public void SetLevelVolume(float value)
    {
        gamePlayerSt.SetLevelVolume(value);
    }


    public void SetLevelVolumeMusic(float value)
    {
        gamePlayerSt.SetLevelVolumeMusica(value);
    }


    public void OpenSettings()
    {
        setMenusActiveOrDesactive(true, false, false);
    }

    public void OpenKeys()
    {
        setMenusActiveOrDesactive(false, true, false);
    }

    public void OpenGrafics()
    {
        setMenusActiveOrDesactive(false, false, true);
    }

    public void SalvarDados()
    {
        gamePlayerSt.SaveData(salvarDadosSettings.LoadData());
        keysClass.SaveData(salvarDadosSettings.LoadData());
        graficsAndLagunages.SaveData(salvarDadosSettings.LoadData());
        salvarDadosSettings.SaveData();
    }


    private void OnGUI()
    {
        keysClass.OnGUI();
    }

}

[System.Serializable] public class GamePlayerSt : Menu
{

    [Header("PAINEL MENU")]
    [SerializeField] private GameObject PainelMenuSettigns;

    [Space(1)]
    [Header("SLDIERS")]

    [SerializeField] private Slider sliderSensitive;
    [SerializeField] private Slider SliderMusic;
    [SerializeField] private Slider SliderVolumeGeral;
    [SerializeField] private Slider fovCameraGame;

    [Space(1)]
    [Header("TO ASSING")]
    [SerializeField] private AudioMixer[] audioMixers;

 
    public void SetLevelVolume(float Value)
    {
        foreach(AudioMixer audio in audioMixers)
        {
            audio.SetFloat("Volume", Mathf.Log10(Value) * 20);
        }
    }

    public void SetLevelVolumeMusica(float Value)
    {
        foreach (AudioMixer audio in audioMixers)
        {
            audio.SetFloat("Musica", Mathf.Log10(Value) * 20);
        }
    }

    public void SetMenuActiveOrDesactive(bool isActive)
    {
        PainelMenuSettigns.SetActive(isActive);
    }

    public void LoadData(DataDados data)
    {
        sliderSensitive.value = data.saveSensitive;
        SliderMusic.value = data.SaveVolumeMusic;
        SliderVolumeGeral.value = data.SaveVoolumeGeral;

        SetLevelVolume(data.SaveVoolumeGeral);
        SetLevelVolumeMusica(data.SaveVolumeMusic);
        fovCameraGame.value = data.saveFov;

    }

    public void SaveData(DataDados data)
    {
        data.saveSensitive = sliderSensitive.value;
        data.SaveVolumeMusic = SliderMusic.value;
        data.SaveVoolumeGeral = SliderVolumeGeral.value;
        data.saveFov = fovCameraGame.value;
    }

 
}

[System.Serializable] public class GraficsAndLagunages : Menu
{
    [Header("configurar")]
    [SerializeField] private Dropdown Idioma;
    [SerializeField] private Toggle isFullScrew;
    [SerializeField] private Dropdown Resolution, graficos, vsync;

    [SerializeField] private GameObject MenGrafics;


    public void SetnewGrafics()
    {
        QualitySettings.SetQualityLevel(Resolution.value);
        QualitySettings.vSyncCount = vsync.value;
    }

    public void SetNewResolution()
    {
        switch (Resolution.value)
        {
            case 0:
                Screen.SetResolution(800, 600, isFullScrew);
                break;
            case 1:
                Screen.SetResolution(1024, 768, isFullScrew);
                break;
            case 2:
                Screen.SetResolution(1280, 720, isFullScrew);
                break;
            case 3:
                Screen.SetResolution(1280, 960, isFullScrew);
                break;
            case 4:
                Screen.SetResolution(1280, 1024, isFullScrew);
                break;
            case 5:
                Screen.SetResolution(1360, 768, isFullScrew);
                break;
            case 6:
                Screen.SetResolution(1600, 900, isFullScrew);
                break;
            case 7:
                Screen.SetResolution(1600, 1050, isFullScrew);
                break;
            case 8:
                Screen.SetResolution(1720, 1080, isFullScrew);
                break;
            case 9:
                Screen.SetResolution(1920, 1080, isFullScrew);
                break;



        }
    }

    public void SetIdioma()
    {
        
    }

    public void LoadData(DataDados data)
    {
        Idioma.value = data.saveIdioma;
        isFullScrew.isOn = data.saveFullScrew;
        Resolution.value = data.saveResolution;
        graficos.value = data.saveGrafics;
        vsync.value = data.saveVSync;
    }

    public void SaveData(DataDados data)
    {
        data.saveFullScrew = isFullScrew.isOn;
        data.saveIdioma = Idioma.value;
        data.saveResolution = Resolution.value;
        data.saveGrafics = graficos.value;
        data.saveVSync = vsync.value;
    }

    public void SetMenuActiveOrDesactive(bool isActive)
    {
        MenGrafics.SetActive(isActive);
    }
}

[System.Serializable]public class KeysClass : Menu
{
    [Header("configurar")]
    [SerializeField] private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private List<keysCodeOne> KeybordKeysCurrect;

    private GameObject MenuGrafics;
    private GameObject currectKey;

    public KeysClass(List<keysCodeOne> Keysy,GameObject Menu)
    {
        KeybordKeysCurrect = Keysy;
        MenuGrafics = Menu;

        foreach(keysCodeOne one in Keysy)
        {
            keys[one.nameKey] = one.key;
            one.keyBoardTxt.text = one.key.ToString();
        }
    }

    public void OnGUI()
    {
        if (!currectKey)
            return;

        Event e = Event.current;

        if (e.isKey)
        {
            keys[currectKey.name] = e.keyCode;
            KeybordKeysCurrect.Find(FindKeyIquals).key = e.keyCode;
            KeybordKeysCurrect.Find(FindKeyIquals).keyBoardTxt.text = e.keyCode.ToString();
            currectKey = null;
        }else 
        if (e.isMouse)
        {
            switch (e.button)
            {
                case 0:
                    keys[currectKey.name] = (KeyCode)323;
                    currectKey.transform.GetChild(0).GetComponent<Text>().text = "MOUSE 0";
                    break;
                case 1:
                    keys[currectKey.name] = (KeyCode)324;
                    currectKey.transform.GetChild(0).GetComponent<Text>().text = "MOUSE 1";
                    break;
                case 2:
                    keys[currectKey.name] = (KeyCode)325;
                    currectKey.transform.GetChild(0).GetComponent<Text>().text = "MOUSE 2";
                    break;
            }

            currectKey = null;
        }


        bool FindKeyIquals(keysCodeOne io)
        {
            return io.nameKey.Equals(currectKey.name);
        }

    }


    public void ChargeKey(GameObject currecKey)
    {
        currectKey = currecKey;
        return;
    }

    public void LoadData(DataDados data)
    {
        int i = 0;
        foreach(keysCodeOne one in data.keysCodeOnes)
        {
            keys[one.nameKey] = one.key;
            KeybordKeysCurrect[i].keyBoardTxt.text = one.key.ToString();
            KeybordKeysCurrect[i].key = one.key;
            i++;
        }
    }

    public void SaveData(DataDados data)
    {
        data.keysCodeOnes = KeybordKeysCurrect;
    }

    public void SetMenuActiveOrDesactive(bool isActive)
    {
        MenuGrafics.SetActive(isActive);
    }
}

[System.Serializable] public class keysCodeOne
{
    public string nameKey;
    public Text keyBoardTxt;
    public KeyCode key;
}


public interface Menu
{
    void SetMenuActiveOrDesactive(bool isActive);
    void LoadData(DataDados data);
    void SaveData(DataDados data);
        
}

[System.Serializable] public class SalvarDadosSettings
{
    [SerializeField] private DataDados dataDados = new DataDados();

    public void SaveData()
    {
        string json = JsonUtility.ToJson(dataDados);
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath,"config.txt"), json);
    }

    public DataDados LoadDataFirst()
    {
        string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "config.txt"));
        JsonUtility.FromJsonOverwrite(json, dataDados);

        return dataDados;
    }

    public DataDados LoadData()
    {
        return dataDados;
    }


}

[System.Serializable] public class DataDados
{

    [SerializeField] public int saveResolution;
    [SerializeField] public int saveGrafics;
    [SerializeField] public bool saveFullScrew;
    [SerializeField] public int saveVSync;

    [SerializeField] public float saveSensitive;
    [SerializeField] public int saveIdioma;
    [SerializeField] public float saveFov,SaveVoolumeGeral,SaveVolumeMusic;

    [SerializeField] public List<keysCodeOne> keysCodeOnes = new List<keysCodeOne>();


}

