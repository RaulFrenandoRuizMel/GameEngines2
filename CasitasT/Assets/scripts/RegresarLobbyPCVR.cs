using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;


public class LanzadorJuegoPCVR : MonoBehaviour
{
    // =====================================================
    // REGRESO AL LOBBY CON Y
    // =====================================================

    [Header("Regreso al Lobby")]

    [Tooltip(
        "Activalo solamente dentro de los juegos. " +
        "En el Lobby dejalo apagado."
    )]
    public bool regresarConYIzquierda = false;


    [Tooltip(
        "Ruta RELATIVA desde el EXE del juego actual hasta el Lobby."
    )]
    public string rutaRelativaLobby =
        @"..\..\LOBBYEXES\LobbyCasitasDelTerror.exe";


    // =====================================================
    // TIEMPOS
    // =====================================================

    [Header("Tiempos")]

    public float esperaLiberarXR = 0.8f;

    public float esperaConfirmarProceso = 1.5f;


    // =====================================================
    // VARIABLES INTERNAS
    // =====================================================

    private InputDevice controlIzquierdo;

    private bool yAnterior = false;

    private bool cambiandoAplicacion = false;


    // =====================================================
    // WINDOWS API
    // =====================================================

    [StructLayout(LayoutKind.Sequential)]
    private struct STARTUPINFO
    {
        public int cb;

        public System.IntPtr lpReserved;
        public System.IntPtr lpDesktop;
        public System.IntPtr lpTitle;

        public int dwX;
        public int dwY;
        public int dwXSize;
        public int dwYSize;
        public int dwXCountChars;
        public int dwYCountChars;
        public int dwFillAttribute;
        public int dwFlags;

        public short wShowWindow;
        public short cbReserved2;

        public System.IntPtr lpReserved2;
        public System.IntPtr hStdInput;
        public System.IntPtr hStdOutput;
        public System.IntPtr hStdError;
    }


    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESS_INFORMATION
    {
        public System.IntPtr hProcess;
        public System.IntPtr hThread;

        public int dwProcessId;
        public int dwThreadId;
    }


    [DllImport(
        "kernel32.dll",
        CharSet = CharSet.Unicode,
        SetLastError = true
    )]
    private static extern bool CreateProcessW(
        string lpApplicationName,
        StringBuilder lpCommandLine,
        System.IntPtr lpProcessAttributes,
        System.IntPtr lpThreadAttributes,
        bool bInheritHandles,
        uint dwCreationFlags,
        System.IntPtr lpEnvironment,
        string lpCurrentDirectory,
        ref STARTUPINFO lpStartupInfo,
        out PROCESS_INFORMATION lpProcessInformation
    );


    [DllImport(
        "kernel32.dll",
        SetLastError = true
    )]
    private static extern uint WaitForSingleObject(
        System.IntPtr hHandle,
        uint dwMilliseconds
    );


    [DllImport(
        "kernel32.dll",
        SetLastError = true
    )]
    private static extern bool CloseHandle(
        System.IntPtr hObject
    );


    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentProcessId();


    [DllImport(
        "kernel32.dll",
        SetLastError = true
    )]
    private static extern System.IntPtr OpenProcess(
        uint dwDesiredAccess,
        bool bInheritHandle,
        uint dwProcessId
    );


    [DllImport(
        "kernel32.dll",
        SetLastError = true
    )]
    private static extern bool TerminateProcess(
        System.IntPtr hProcess,
        uint uExitCode
    );


    private const uint WAIT_TIMEOUT =
        0x00000102;


    private const uint PROCESS_TERMINATE =
        0x0001;


    // =====================================================
    // START
    // =====================================================

    void Start()
    {
        Application.runInBackground = true;


        if (regresarConYIzquierda)
        {
            BuscarControlIzquierdo();
        }
    }


    // =====================================================
    // UPDATE
    // =====================================================

    void Update()
    {
        if (!regresarConYIzquierda)
            return;


        if (!controlIzquierdo.isValid)
        {
            BuscarControlIzquierdo();
        }


        LeerBotonY();
    }


    // =====================================================
    // BUSCAR CONTROL IZQUIERDO
    // =====================================================

    void BuscarControlIzquierdo()
    {
        controlIzquierdo =
            InputDevices.GetDeviceAtXRNode(
                XRNode.LeftHand
            );


        if (controlIzquierdo.isValid)
        {
            Debug.Log(
                "CONTROL IZQUIERDO DETECTADO: " +
                controlIzquierdo.name
            );
        }
    }


    // =====================================================
    // BOTON Y
    // =====================================================

    void LeerBotonY()
    {
        bool yPresionado = false;


        bool recibido =
            controlIzquierdo.TryGetFeatureValue(
                CommonUsages.secondaryButton,
                out yPresionado
            );


        if (!recibido)
            return;


        if (yPresionado && !yAnterior)
        {
            Debug.Log(
                "Y IZQUIERDA -> REGRESANDO AL LOBBY"
            );


            // =============================================
            // SOLO PARA REGRESAR AL LOBBY
            // =============================================

            LanzarRuta(
                rutaRelativaLobby
            );
        }


        yAnterior =
            yPresionado;
    }


    // =====================================================
    // FUNCION UNIVERSAL
    // =====================================================
    //
    // Las puertas también usan esta función.
    //
    // DestinoJuegoPuerta manda:
    //
    // ..\Equipos\EXEEQUIPO4\Equipo4.exe
    //
    // Y para regresar:
    //
    // ..\..\LOBBYEXES\LobbyCasitasDelTerror.exe
    //
    // =====================================================

    public void LanzarRuta(
        string rutaRelativa
    )
    {
        if (cambiandoAplicacion)
        {
            Debug.LogWarning(
                "YA HAY UN CAMBIO DE APLICACION EN CURSO"
            );

            return;
        }


        if (string.IsNullOrWhiteSpace(
            rutaRelativa
        ))
        {
            Debug.LogError(
                "LA RUTA RELATIVA ESTA VACIA"
            );

            return;
        }


        // =================================================
        // CARPETA DEL EXE QUE ESTA CORRIENDO
        // =================================================

        string carpetaExeActual =
            Directory.GetParent(
                Application.dataPath
            ).FullName;


        // =================================================
        // RESOLVER RUTA RELATIVA
        // =================================================

        string rutaCompleta =
            Path.GetFullPath(
                Path.Combine(
                    carpetaExeActual,
                    rutaRelativa
                )
            );


        Debug.Log(
            "CARPETA DEL EXE ACTUAL:\n" +
            carpetaExeActual
        );


        Debug.Log(
            "RUTA RELATIVA RECIBIDA:\n" +
            rutaRelativa
        );


        Debug.Log(
            "RUTA FINAL:\n" +
            rutaCompleta
        );


        StartCoroutine(
            CambiarAplicacion(
                rutaCompleta
            )
        );
    }


    // =====================================================
    // CAMBIAR APLICACION
    // =====================================================

    IEnumerator CambiarAplicacion(
        string rutaDestino
    )
    {
        cambiandoAplicacion = true;


        // =================================================
        // 1. VERIFICAR DESTINO
        // =================================================

        if (!File.Exists(
            rutaDestino
        ))
        {
            Debug.LogError(
                "NO EXISTE EL EXE:\n" +
                rutaDestino
            );


            cambiandoAplicacion = false;

            yield break;
        }


        Debug.Log(
            "DESTINO ENCONTRADO:\n" +
            rutaDestino
        );


        // =================================================
        // 2. LIBERAR OPENXR
        // =================================================

        XRManagerSettings manager = null;


        if (XRGeneralSettings.Instance != null)
        {
            manager =
                XRGeneralSettings
                    .Instance
                    .Manager;
        }


        if (manager != null &&
            manager.isInitializationComplete)
        {
            Debug.Log(
                "LIBERANDO XR..."
            );


            manager.StopSubsystems();

            manager.DeinitializeLoader();
        }


        yield return
            new WaitForSecondsRealtime(
                esperaLiberarXR
            );


        // =================================================
        // 3. PREPARAR EXE
        // =================================================

        STARTUPINFO startup =
            new STARTUPINFO();


        startup.cb =
            Marshal.SizeOf(
                typeof(STARTUPINFO)
            );


        StringBuilder lineaComandos =
            new StringBuilder(
                "\"" +
                rutaDestino +
                "\""
            );


        PROCESS_INFORMATION proceso;


        // =================================================
        // 4. ABRIR DESTINO
        // =================================================

        bool abierto =
            CreateProcessW(
                rutaDestino,
                lineaComandos,
                System.IntPtr.Zero,
                System.IntPtr.Zero,
                false,
                0,
                System.IntPtr.Zero,
                Path.GetDirectoryName(
                    rutaDestino
                ),
                ref startup,
                out proceso
            );


        if (!abierto)
        {
            int error =
                Marshal.GetLastWin32Error();


            Debug.LogError(
                "NO SE PUDO ABRIR EL DESTINO. " +
                "ERROR WINDOWS: " +
                error
            );


            cambiandoAplicacion = false;


            yield return
                StartCoroutine(
                    RecuperarXR()
                );


            yield break;
        }


        // No necesitamos el thread handle.
        CloseHandle(
            proceso.hThread
        );


        Debug.Log(
            "NUEVO EXE ABIERTO. PID: " +
            proceso.dwProcessId
        );


        Debug.Log(
            "RUTA EJECUTADA:\n" +
            rutaDestino
        );


        // =================================================
        // 5. ESPERAR Y CONFIRMAR
        // =================================================

        yield return
            new WaitForSecondsRealtime(
                esperaConfirmarProceso
            );


        bool sigueVivo =
            WaitForSingleObject(
                proceso.hProcess,
                0
            ) == WAIT_TIMEOUT;


        if (!sigueVivo)
        {
            Debug.LogError(
                "EL NUEVO EXE SE CERRO. " +
                "NO CIERRO LA APP ACTUAL."
            );


            CloseHandle(
                proceso.hProcess
            );


            cambiandoAplicacion = false;


            yield return
                StartCoroutine(
                    RecuperarXR()
                );


            yield break;
        }


        CloseHandle(
            proceso.hProcess
        );


        Debug.Log(
            "CAMBIO CORRECTO. CERRANDO APP ACTUAL."
        );


        // =================================================
        // 6. CERRAR APP ACTUAL
        // =================================================

        CerrarAplicacionActual();
    }


    // =====================================================
    // CERRAR APP ACTUAL
    // =====================================================

    void CerrarAplicacionActual()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying =
            false;

#else

        uint pid =
            GetCurrentProcessId();


        System.IntPtr handle =
            OpenProcess(
                PROCESS_TERMINATE,
                false,
                pid
            );


        if (handle != System.IntPtr.Zero)
        {
            TerminateProcess(
                handle,
                0
            );


            CloseHandle(
                handle
            );
        }
        else
        {
            Application.Quit();
        }

#endif
    }


    // =====================================================
    // RECUPERAR XR SI FALLA EL NUEVO EXE
    // =====================================================

    IEnumerator RecuperarXR()
    {
        Debug.Log(
            "RECUPERANDO XR..."
        );


        if (XRGeneralSettings.Instance == null)
        {
            yield break;
        }


        XRManagerSettings manager =
            XRGeneralSettings
                .Instance
                .Manager;


        if (manager == null)
        {
            yield break;
        }


        yield return
            manager.InitializeLoader();


        if (manager.activeLoader == null)
        {
            Debug.LogError(
                "NO SE PUDO RECUPERAR XR"
            );


            yield break;
        }


        manager.StartSubsystems();


        Debug.Log(
            "XR RECUPERADO"
        );
    }
}