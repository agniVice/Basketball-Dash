using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUserInterface : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _settingsPanel;

    [Header("Settings")]
    [SerializeField] private Button _soundToggle;
    [SerializeField] private Button _musicToggle;
    [SerializeField] private Button _vibrationToggle;

    [SerializeField] private Sprite _soundEnabled;
    [SerializeField] private Sprite _soundDisabled;

    [SerializeField] private Sprite _musicEnabled;
    [SerializeField] private Sprite _musicDisabled;

    [SerializeField] private Sprite _vibrationEnabled;
    [SerializeField] private Sprite _vibrationDisabled;


    [SerializeField] private Transform _dropDownPanel;

    [SerializeField] private Image _playButtonImage;
    [SerializeField] private Sprite[] _playButtonSprites;

    [SerializeField] private Transform _playArrow;
    [SerializeField] private float[] _playArrowAngles;


    [SerializeField] private List<Transform> _transformsMenu = new List<Transform>();
    [SerializeField] private List<Transform> _transformsSettings = new List<Transform>();

    private bool _dropDownOpened = false;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        AudioVibrationManager.Instance.SoundChanged += UpdateSoundImage;
        AudioVibrationManager.Instance.MusicChanged += UpdateMusicImage;
        //AudioVibrationManager.Instance.VibrationChanged += UpdateVibrationImage;

        _soundToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleSound);
        _musicToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleMusic);
        //_vibrationToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleVibration);

        UpdateSoundImage(); 
        UpdateMusicImage();
        UpdateLevelDifficulty(LevelManager.Instance.LevelDifficulty);
        //UpdateVibrationImage();
        //UpdateLanguageIcon();

        OnCloseSettingsButtonClicked();
    }
    private void OnDisable()
    {
        AudioVibrationManager.Instance.SoundChanged -= UpdateSoundImage;
        AudioVibrationManager.Instance.MusicChanged -= UpdateMusicImage;
        //AudioVibrationManager.Instance.VibrationChanged -= UpdateVibrationImage;
    }
    private void UpdateSoundImage()
    {
        if (AudioVibrationManager.Instance.IsSoundEnabled) 
            _soundToggle.GetComponent<Image>().sprite = _soundEnabled;
        else
            _soundToggle.GetComponent<Image>().sprite = _soundDisabled;
    }
    private void UpdateMusicImage()
    {
        if (AudioVibrationManager.Instance.IsMusicEnabled)
            _musicToggle.GetComponent<Image>().sprite = _musicEnabled;
        else
            _musicToggle.GetComponent<Image>().sprite = _musicDisabled;
    }
    private void UpdateVibrationImage()
    {
        if (AudioVibrationManager.Instance.IsVibrationEnabled)
            _vibrationToggle.GetComponent<Image>().sprite = _vibrationEnabled;
        else
            _vibrationToggle.GetComponent<Image>().sprite = _vibrationDisabled;
    }
    public void OnSettingsButtonClicked()
    {
        _menuPanel.SetActive(false);
        _settingsPanel.SetActive(true);

        foreach (Transform transform in _transformsSettings)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, Random.Range(0.2f, 0.7f)).SetEase(Ease.OutBack).SetLink(transform.gameObject).SetUpdate(true);
        }
    }
    public void OnCloseSettingsButtonClicked()
    {
        _menuPanel.SetActive(true);
        _settingsPanel.SetActive(false);

        foreach (Transform transform in _transformsMenu)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, Random.Range(0.2f, 0.7f)).SetEase(Ease.OutBack).SetLink(transform.gameObject).SetUpdate(true);
        }
    }
    public void OnPlayButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }
    public void OnInputLanguage(int value)
    {
        LanguageManager.Instance.ChangeLanguage(value);
        DropDownInteract();
    }
    public void UpdateLevelDifficulty(int difficulty)
    {
        foreach(Transform t in _transformsMenu)
            t.DOShakePosition(0.2f, 0.1f, fadeOut: true).SetUpdate(true);

        _playButtonImage.transform.DOScale(1.3f, 0.3f).SetLink(_playButtonImage.gameObject).SetEase(Ease.OutBack);
        _playButtonImage.transform.DOScale(1, 0.3f).SetLink(_playButtonImage.gameObject).SetEase(Ease.OutBack).SetDelay(0.3f);

        _playButtonImage.sprite = _playButtonSprites[difficulty];
        _playArrow.DOLocalRotate(new Vector3(0, 0, _playArrowAngles[difficulty]), 0.2f).SetLink(_playArrow.gameObject).SetEase(Ease.OutBack);
    }
    public void UpdateLanguageIcon()
    {

    }
    public void DropDownInteract()
    {
        _dropDownOpened = !_dropDownOpened;
        _dropDownPanel.gameObject.SetActive(_dropDownOpened);
        UpdateLanguageIcon();
    }
    public void OnExitButtonClicked()
    { 
        Application.Quit();
    }
}
