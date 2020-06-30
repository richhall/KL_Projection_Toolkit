using RockVR.Common;
using UnityEngine;


namespace PI.ProjectionToolkit
{
    /// <summary>
    /// Base class for <c>VideoCaptureCtrl</c> and <c>VideoCaptureProCtrl</c> class.
    /// </summary>
    public class VirtualProjectorCtrlBase : Singleton<VirtualProjectorCtrlBase>
    {
        /// <summary>
        ///                   NOT_START
        ///                      |
        ///                      | StartCapture()
        ///                      |
        ///    StartCapture()    v
        ///  ---------------> STARTED
        ///  |                   |
        ///  |                   | StopCapture()
        ///  |                   |
        ///  |                   v
        ///  |                STOPPED
        ///  |                   |
        ///  |                   | Process?
        ///  |                   |
        ///  |                   v
        ///  ----------------- FINISH
        /// </summary>
        public enum StatusType
        {
            NOT_START,
            STARTED,
            PAUSED,
            STOPPED,
            FINISH,
        }
        /// <summary>
        /// Indicates the error of <c>VideoCaptureCtrl</c> module.
        /// </summary>
        public enum ErrorCodeType
        {
            /// <summary>
            /// No camera or audio was found to perform video or audio
            /// recording. You must specify one or more to start record.
            /// </summary>
            PROJECTOR_NOT_FOUND = -1,

        }
        /// <summary>
        /// Get or set the current status.
        /// </summary>
        /// <value>The current status.</value>
        public StatusType status { get; protected set; }
        /// <summary>
        /// Enable debug message.
        /// </summary>
        public bool debug = false;
        /// <summary>
        /// Whether start capture on awake.
        /// </summary>
        public bool startOnAwake = false;
        /// <summary>
        /// The capture time.
        /// </summary>
        public float captureTime = 10f;
        /// <summary>
        /// Whether quit process after capture finish。
        /// </summary>
        public bool quitAfterCapture = false;
        /// <summary>
        /// Delegate to register event.
        /// </summary>
        public EventDelegate eventDelegate = new EventDelegate();
        /// <summary>
        /// Reference to the <c>VideoCapture</c> or <c>VideoCapturePro</c> components
        /// (i.e. cameras) which will be recorded.
        /// Generally you will want to specify at least one.
        /// </summary>
        [SerializeField]
     //   private VirtualProjectorBase[] _virtualProjectors;
        /// <summary>
        /// Get or set the <c>VideoCapture</c> or <c>VideoCapturePro</c> components.
        /// </summary>
        /// <value>The <c>VideoCapture</c> components.</value>
    /*    public VirtualProjectorBase[] videoCaptures
        {
            get
            {
                return _virtualProjectors;
            }
            set
            {
                if (status == StatusType.STARTED)
                {
                    Debug.LogWarning("[VirtualProjectorCtrl::VirtualProjector] Cannot " +
                                     "set proejctor druring replay session!");
                    return;
                }
                _virtualProjectors = value;
            }
        } */
        /// <summary>
        /// Start projection process.
        /// </summary>
        public virtual void StartProjection() { }
        /// <summary>
        /// Stop projection process.
        /// </summary>
        public virtual void StopProjection() { }
        /// <summary>
        /// Pause capture process.
        /// </summary>
        public virtual void ToggleProjection() { }

        private void Start()
        {
            if (startOnAwake && status == StatusType.NOT_START)
            {
                StartProjection();
            }
        }

        private void Update()
        {
            if (startOnAwake)
            {
                if (Time.time >= captureTime && status == StatusType.STARTED)
                {
                    StopProjection();
                }
                if (status == StatusType.FINISH && quitAfterCapture)
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                }
            }
        }
    }
}