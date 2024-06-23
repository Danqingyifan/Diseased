using UnityEngine;

namespace Fungus
{
    [RequireComponent(typeof(CameraManager))]
    [RequireComponent(typeof(MusicManager))]
    [RequireComponent(typeof(EventDispatcher))]
    [RequireComponent(typeof(GlobalVariables))]
#if UNITY_5_3_OR_NEWER
    [RequireComponent(typeof(SaveManager))]
    [RequireComponent(typeof(NarrativeLog))]
#endif
    public sealed class FungusManager : MonoBehaviour
    {
        private static FungusManager instance;
        private static bool applicationIsQuitting = false;
        private static readonly object _lock = new object();
        

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            CameraManager = GetComponent<CameraManager>();
            MusicManager = GetComponent<MusicManager>();
            EventDispatcher = GetComponent<EventDispatcher>();
            GlobalVariables = GetComponent<GlobalVariables>();

#if UNITY_5_3_OR_NEWER
            SaveManager = GetComponent<SaveManager>();
            NarrativeLog = GetComponent<NarrativeLog>();
#endif
        }

        void OnDestroy()
        {
            if (instance == this)
            { 
                //applicationIsQuitting = true;
            }
        }

        #region Public properties

        public CameraManager CameraManager { get; private set; }
        public MusicManager MusicManager { get; private set; }
        public EventDispatcher EventDispatcher { get; private set; }
        public GlobalVariables GlobalVariables { get; private set; }

#if UNITY_5_3_OR_NEWER
        public SaveManager SaveManager { get; private set; }
        public NarrativeLog NarrativeLog { get; private set; }
#endif

        public static FungusManager Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("FungusManager.Instance() was called while application is quitting. Returning null instead.");
                    return null;
                }

                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<FungusManager>();

                        if (instance == null)
                        {
                            var go = new GameObject("FungusManager");
                            DontDestroyOnLoad(go);
                            instance = go.AddComponent<FungusManager>();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion
    }
}
