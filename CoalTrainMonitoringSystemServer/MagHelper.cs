using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using SDK;

namespace CoalTrainMonitoringSystemServer
{
    public interface IMagService
    {
        bool Initialize();
        void DeInitialize();
        bool IsInitialized();

        bool IsLanConnected();
        bool IsUsingStaticIp();
        uint GetLocalIp();

        bool IsDHCPServerRunning();
        bool StartDHCPServer();
        void StopDHCPServer();

        void EnableAutoReConnect(bool bEnable);

        int CompressDDT(IntPtr pDstBuffer, uint intDstBufferSize, IntPtr pSrcBuffer, uint intSrcBufferSize, uint intQuality);
        int DeCompressDDT(IntPtr pDstBuffer, uint intDstBufferSize, IntPtr pSrcBuffer, uint intSrcBufferSize);

        bool EnumCameras();
        uint GetTerminalList(GroupSDK.ENUM_INFO[] list, uint unit_count);
        uint GetTerminalCount();
        bool GetMulticastState(uint intTargetIP, ref uint intMulticastIp, ref uint intMulticastPort, uint intTimeout);
    }

    public interface IMagDevice
    {
        bool Initialize();
        void DeInitialize();
        bool IsInitialized();

        GroupSDK.CAMERA_INFO GetCamInfo();
        GroupSDK.CAMERA_REGCONTENT GetRegContent();

        void ConvertPos2XY(uint intPos, ref uint x, ref uint y);
        uint ConvertXY2Pos(uint x, uint y);

        bool IsLinked();

        bool LinkCamera(string sIP, uint intTimeOut);
        bool LinkCamera(uint intIP, uint intTimeOut);

        bool LinkCameraEx(string sIP, ushort shortCmdPort, ushort shortImgPort, string charCloudUser, string charCloudPwd,
            uint intCamSN, string charCamUser, string charCamPwd, uint intTimeOut);
        bool LinkCameraEx(uint intIP, ushort shortCmdPort, ushort shortImgPort, string charCloudUser, string charCloudPwd,
            uint intCamSN, string charCamUser, string charCamPwd, uint intTimeOut);

        void DisLinkCamera();
        uint GetRecentHeartBeat();

        bool IsListening();
	    bool ListenTo(uint intIP);
	    void StopListen();

	    bool ResetCamera();
	    bool TriggerFFC();
	    bool AutoFocus();
	    bool SetPTZCmd(GroupSDK.PTZIRCMD cmd, uint dwPara);
	    bool QueryPTZState(GroupSDK.PTZQuery query, ref int intValue, uint intTimeout);
	    bool SetSerialCmd(byte[] buffer, uint intBufferLen);
    	
	    bool GetCameraTemperature([MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]int[] intT, uint intTimeout);
	    bool SetCameraRegContent(GroupSDK.CAMERA_REGCONTENT RegContent);

	    bool SetUserROIs(GroupSDK.USER_ROI roi);

	    bool IsProcessingImage();
	    bool StartProcessImage(GroupSDK.OUTPUT_PARAM param, GroupSDK.DelegateNewFrame funcFrame, uint dwStreamType, IntPtr pUserData);
	    bool StartProcessPulseImage(GroupSDK.OUTPUT_PARAM param, GroupSDK.DelegateNewFrame funcFrame, uint dwStreamType, IntPtr pUserData);
	    bool TransferPulseImage();
	    void StopProcessImage();

	    void SetColorPalette(GroupSDK.COLOR_PALETTE ColorPaletteIndex);
	    bool SetSubsectionEnlargePara(int intX1, int intX2, byte byteY1, byte byteY2);
	    void SetAutoEnlargePara(uint dwAutoEnlargeRange, int intBrightOffset, int intContrastOffset);
	    void SetEXLevel(GroupSDK.EX ex, int intCenterX, int intCenterY);
	    GroupSDK.EX GetEXLevel();
	    void SetDetailEnhancement(int intDDE, int isQuickDDE);
	    bool SetVideoContrast(int intContrastOffset);
	    bool SetVideoBrightness(int intBrightnessOffset);

	    void GetFixPara(ref GroupSDK.FIX_PARAM param);
        float SetFixPara(ref GroupSDK.FIX_PARAM param, bool isEnableCameraCorrect);
	    int FixTemperature(int intT, float fEmissivity, uint dwPosX, uint dwPosY);

	    bool GetOutputBMPdata(ref IntPtr pData, ref IntPtr pInfo);
	    bool GetOutputColorBardata(ref IntPtr pData, ref IntPtr pInfo);
	    bool GetOutputVideoData(ref IntPtr pData, ref IntPtr pInfo);

	    uint GetDevIPAddress();
	    GroupSDK.TEMP_STATE GetFrameStatisticalData();
	    bool GetTemperatureData(int[] data, uint intBufferSize, int isEnableExtCorrect);
	    bool GetTemperatureDataRaw(int[] data, uint intBufferSize, int isEnableExtCorrect);
	    int GetTemperatureProbe(uint dwPosX, uint dwPosY, uint intSize);
	    int GetLineTemperatureInfo(int[] buffer, uint intBufferSizeByte, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)]int[] info, uint x0, uint y0, uint x1, uint y1);
	    bool GetRectTemperatureInfo(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info);
	    bool GetEllipseTemperatureInfo(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info);
	    bool GetRgnTemperatureInfo(uint[] pos, uint intPosNumber, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info);	

	    bool UseTemperatureMask(int isUse);
	    bool IsUsingTemperatureMask();

	    bool SaveBMP(uint dwIndex, [MarshalAs(UnmanagedType.LPWStr)] string sFileName);
	    bool SaveMGT([MarshalAs(UnmanagedType.LPWStr)] string sFileName);
	    bool SaveDDT([MarshalAs(UnmanagedType.LPWStr)] string sFileName);
	    int SaveDDT2Buffer(byte[] buffer, uint intBufferSize);
	    bool LoadDDT(GroupSDK.OUTPUT_PARAM param, [MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData);
	    bool LoadBufferedDDT(GroupSDK.OUTPUT_PARAM param, IntPtr pBuffer, uint size, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData);

        bool SetAsyncCompressCallBack(uint intChannelIndex, GroupSDK.DelegateDDTCompressComplete funcDDTCompressComplete, int intQuality);
        bool GrabAndAsyncCompressDDT(uint intChannelIndex, IntPtr pUserData);

        bool LocalStorageAviStart([MarshalAs(UnmanagedType.LPWStr)] string sFileName, uint samplePeriod);
        void LocalStorageAviStop();
        bool LocalStorageMgsRecord([MarshalAs(UnmanagedType.LPWStr)] string sFileName, uint samplePeriod);
        int  LocalStorageMgsPlay([MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData);
        bool LocalStorageMgsSeekFrame(uint hFrame);
        bool LocalStorageMgsPopFrame();
        void LocalStorageMgsStop();

	    bool SDStorageMGT();
	    bool SDStorageBMP();
	    bool SDStorageMGSStart();
	    bool SDStorageMGSStop();
	    bool SDStorageAviStart();
	    bool SDStorageAviStop();
        bool SDCardStorage(uint hDevice, GroupSDK.SDStorageFileType filetype, uint para);

        void SetIsothermalPara(int intLowerLimit, int intUpperLimit);
        bool SetSerialCallBack(GroupSDK.DelegateSerial cb, IntPtr pUserData);
        bool SetReConnectCallBack(GroupSDK.DelegateReconnect cb, IntPtr pUserData);

        bool GetCurrentOffset([MarshalAs(UnmanagedType.LPWStr)] string sReferenceDDT, ref int offsetx, ref int offsety);

	    void Lock();
	    void Unlock();
    }

    public class MagService : IMagService
    {
        const uint MAG_DEFAULT_TIMEOUT = 500;//默认溢出时间
        static bool m_bIsIntialized = false; //是否被初始化 初始值为否
        IntPtr m_hWndMsg = IntPtr.Zero;  //C#中的IntPtr类型称为“平台特定的整数类型”,它们用于本机资源,如窗口句柄。    

        //构造函数
        public MagService(IntPtr hWndMsg)
        {
            m_hWndMsg = hWndMsg;
        }

        //初始化
        public bool Initialize()
        {
            if (m_bIsIntialized) //如果初始化 返回true
            {
                return true;
            }

            if (!GroupSDK.MAG_IsChannelAvailable(0))//判断通道0是否已调用过MAG_NewChannel ()。
            {
                GroupSDK.MAG_NewChannel(0);//如果通道0没有调用过MAG_NewChannel ()，新建通道0
            }

            if (GroupSDK.MAG_IsLanConnected())//判断计算机是否接入以太网
            {
                //MAG_Initialize(UINT intChannelIndex, HWND hWndMsg)--初始化一个通道，必须在建立通道且接入以太网以后再调用。本函数需与MAG_Free()配对使用。
                //输入参数： intChannelIndex –通道号； hWndMsg-用于接收message的窗口句柄， NULL表示不接收。如果该
                //参数不为NULL，热像仪主动断开连接时向hWndMsg发送message=WM_COMMAND
                //返回值： TRUE-成功； FALSE-失败

                m_bIsIntialized = GroupSDK.MAG_Initialize(0, m_hWndMsg);//如果接入以太网，初始化通道,m_hWndMsg用于接收消息句柄
                //初始化成功返回True给m_bIsIntialized，否则返回False给m_bIsIntialized
            }

            return m_bIsIntialized;
        }

        //解除初始化
        public void DeInitialize()
        {
            //如果没被初始化，返回
            if (!m_bIsIntialized)
            {
                return;
            }
            //MAG_IsDHCPServerRunning()--判断DHCP服务器是否正在运行。
            //功能描述：判断DHCP服务器是否正在运行。
            //输入参数：无。
            //返回值： TRUE-是； FALSE-否。
            if (GroupSDK.MAG_IsDHCPServerRunning())
            {
                GroupSDK.MAG_StopDHCPServer();//如果DHCP服务器正在运行，停止DHCP服务器
            }
            // MAG_IsInitialized(UINT intChannelIndex)
            //功能描述：判断本通道是否已调用过MAG_Initialize()。
            //输入参数： intChannelIndex –通道号。
            //返回值： TRUE-已调用； FALSE-未调用。
            if (GroupSDK.MAG_IsInitialized(0))
            {
                GroupSDK.MAG_Free(0);//如果通道0被初始化，释放通道0
            }
            //MAG_API BOOL WINAPI MAG_NewChannel(UINT intChannelIndex);
            //功能描述： 新建一个通道。通道号必须在0~128之间， 建议首先建立第0通道。 本函数需与MAG_DelChannel()
            //配对使用。
            //输入参数： intChannelIndex –通道号。
            //返回值： TRUE-成功； FALSE-失败。

            //MAG_API void WINAPI MAG_DelChannel(UINT intChannelIndex);
            //功能描述：删除一个通道。
            //输入参数： intChannelIndex –通道号。
            //返回值：无。
            if (GroupSDK.MAG_IsChannelAvailable(0))
            {
                GroupSDK.MAG_DelChannel(0);
            }

            m_bIsIntialized = false;
        }

        public bool IsInitialized()
        {
            return m_bIsIntialized;
        }

        public bool IsLanConnected()
        {
            return GroupSDK.MAG_IsLanConnected();
        }

        /// <summary>
        /// 是否使用静态IP
        /// </summary>
        /// <returns></returns>
        public bool IsUsingStaticIp()
        {
        //MAG_API BOOL WINAPI MAG_IsUsingStaticIp();
        //功能描述：判断计算机是否正在使用静态ip接入以太网。
        //输入参数：无。
        //返回值： TRUE-是； FALSE-否或未使用以太网连接。
            return GroupSDK.MAG_IsUsingStaticIp();
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public uint GetLocalIp()
        {
            if (!m_bIsIntialized)
            {
                Initialize();
            }
            //MAG_API DWORD WINAPI MAG_GetLocalIp();
            //功能描述：获取本地ip，必须在MAG_Initialize()之后才能获取正确结果。 须在第0通道有效且初始化以后方
            //可使用。
            //输入参数：无。
            //返回值： ip，高字节表示ip的最后一节。
            return m_bIsIntialized ? GroupSDK.MAG_GetLocalIp() : 0;
        }

        public bool IsDHCPServerRunning()
        {
            return GroupSDK.MAG_IsDHCPServerRunning();
        }

        public bool StartDHCPServer()
        {
            return GroupSDK.MAG_StartDHCPServer(m_hWndMsg);
        }

        public void StopDHCPServer()
        {
            GroupSDK.MAG_StopDHCPServer();
        }

        /// <summary>
        /// 开启/关闭自动重连功能
        /// </summary>
        /// <param name="bEnable">true 开启  false 关闭</param>
        public void EnableAutoReConnect(bool bEnable)
        {
            GroupSDK.MAG_EnableAutoReConnect(bEnable);
        }

        /// <summary>
        /// 枚举在线相机,异步更新相机列表，因此立即GetTerminalList可能拿不到最新的，推荐sleep（50）,MAG_GetTerminalList()查看结果。
        /// </summary>
        /// <returns></returns>
        public bool EnumCameras()
        {
            if (!m_bIsIntialized)
            {
                Initialize();
            }
            //
            return m_bIsIntialized ? GroupSDK.MAG_EnumCameras() : false;
        }

        /// <summary>
        /// 获取在线相机列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="size">list总的字节数</param>
        /// <returns></returns>
        public uint GetTerminalList(GroupSDK.ENUM_INFO[] list, uint unit_count)
        {
            if (!m_bIsIntialized)
            {
                return 0;
            }

            uint size = (uint)Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)) * unit_count;

            IntPtr ptr = Marshal.AllocHGlobal((int)size);
            IntPtr ptrBackup = ptr;

            uint dev_num = GroupSDK.MAG_GetTerminalList(ptr, size);

            for (int i = 0; i < dev_num; i++)
            {
                list[i] = (GroupSDK.ENUM_INFO)Marshal.PtrToStructure(ptr, typeof(GroupSDK.ENUM_INFO));
                ptr = (IntPtr)((int)ptr + Marshal.SizeOf(typeof(GroupSDK.ENUM_INFO)));
            }

            Marshal.FreeHGlobal(ptrBackup);

            return dev_num;
        }

        public uint GetTerminalCount()
        {
            return m_bIsIntialized ? GroupSDK.MAG_GetTerminalList(IntPtr.Zero, 0) : 0;
        }

        public bool GetMulticastState(uint intTargetIP, ref uint intMulticastIp, ref uint intMulticastPort, uint intTimeout)
        {
            if (!m_bIsIntialized)
            {
                Initialize();
            }

            return m_bIsIntialized ? GroupSDK.MAG_GetMulticastState(0, ref intMulticastIp, ref intMulticastPort, intTimeout) : false;
        }

        public int CompressDDT(IntPtr pDstBuffer, uint intDstBufferSize, IntPtr pSrcBuffer, uint intSrcBufferSize, uint intQuality)
        {
	        return GroupSDK.MAG_CompressDDT(pDstBuffer, intDstBufferSize, pSrcBuffer, intSrcBufferSize, intQuality);
        }

        public int DeCompressDDT(IntPtr pDstBuffer, uint intDstBufferSize, IntPtr pSrcBuffer, uint intSrcBufferSize)
        {
	        return GroupSDK.MAG_DeCompressDDT(pDstBuffer, intDstBufferSize, pSrcBuffer, intSrcBufferSize);
        }
    }

    public class MagDevice : IMagDevice
    {
        const uint MAG_DEFAULT_TIMEOUT = 500;
        const uint MAX_CHANNELINDEX = 32;

        bool m_bIsInitialized = false;
        IntPtr m_hWndMsg = IntPtr.Zero;

        uint m_intChannelIndex = 0xffffffff;
        uint m_intCameraIPAddr = 0;

        GroupSDK.CAMERA_INFO m_CamInfo = new GroupSDK.CAMERA_INFO();
        GroupSDK.CAMERA_REGCONTENT m_RegContent = new GroupSDK.CAMERA_REGCONTENT();

        bool m_bIsRecordingAvi = false;
        bool m_bIsRecordingMgs = false;

        bool m_bIsRecordingLocalAvi = false;
        bool m_bIsRecordingLocalMgs = false;
        bool m_bIsPlayingLocalMgs = false;


        public MagDevice(IntPtr hWndMsg)
        {
            m_hWndMsg = hWndMsg;
        }

        public bool Initialize()
        {
	        if (m_bIsInitialized)
	        {
		        return true;
	        }

            if (m_intChannelIndex <= 0 || m_intChannelIndex > MAX_CHANNELINDEX)
	        {
		        for (uint i = 1; i <= MAX_CHANNELINDEX; i++)
		        {
			        if (!GroupSDK.MAG_IsChannelAvailable(i))//find an unused channel
			        {
				        GroupSDK.MAG_NewChannel(i);
				        m_intChannelIndex = i;

				        break;
			        }
		        }
	        }

	        if (m_intChannelIndex > 0 && m_intChannelIndex <= MAX_CHANNELINDEX && GroupSDK.MAG_IsLanConnected())
	        {
		        m_bIsInitialized = GroupSDK.MAG_Initialize(m_intChannelIndex, m_hWndMsg);
	        }

	        return m_bIsInitialized;
        }

        public void DeInitialize()
        {
            if (!m_bIsInitialized)
            {
                return;
            }

            if (GroupSDK.MAG_IsProcessingImage(m_intChannelIndex))
            {
                GroupSDK.MAG_StopProcessImage(m_intChannelIndex);
            }

            if (GroupSDK.MAG_IsListening(m_intChannelIndex))
            {
                GroupSDK.MAG_StopListen(m_intChannelIndex);
            }

            if (GroupSDK.MAG_IsLinked(m_intChannelIndex))
            {
                DisLinkCamera();//include stop sd storage
            }

            if (GroupSDK.MAG_IsInitialized(m_intChannelIndex))
            {
                GroupSDK.MAG_Free(m_intChannelIndex);
            }

            if (GroupSDK.MAG_IsChannelAvailable(m_intChannelIndex))
            {
                GroupSDK.MAG_DelChannel(m_intChannelIndex);
            }
        }

        public bool IsInitialized()
        {
            return m_bIsInitialized;
        }

        public GroupSDK.CAMERA_INFO GetCamInfo()
        {
            return m_CamInfo;
        }

        public GroupSDK.CAMERA_REGCONTENT GetRegContent()
        {
            return m_RegContent;
        }

        public void ConvertPos2XY(uint intPos, ref uint x, ref uint y)
        {
            uint w = (uint)m_CamInfo.intFPAWidth;

	        if (w != 0)
	        {
		        x = intPos / w;
		        y = intPos - y * w;
	        }
        }

        public uint ConvertXY2Pos(uint x, uint y)
        {
            return y * (uint)m_CamInfo.intFPAWidth + x;
        }

        public bool IsLinked()
        {
            return GroupSDK.MAG_IsLinked(m_intChannelIndex);
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="sIP"></param>
        /// <param name="intTimeOut">ms</param>
        /// <returns></returns>
        public bool LinkCamera(string sIP, uint intTimeOut)
        {
            return LinkCamera(WINAPI.inet_addr(sIP), intTimeOut);
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="intIP"></param>
        /// <param name="intTimeOut">ms</param>
        /// <returns></returns>
        public bool LinkCamera(uint intIP, uint intTimeOut)
        {
            if (GroupSDK.MAG_LinkCamera(m_intChannelIndex, intIP, intTimeOut))
	        {
		        m_intCameraIPAddr = intIP;
		        GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
		        GroupSDK.MAG_ReadCameraRegContent(m_intChannelIndex, ref m_RegContent, intTimeOut, 0);

		        return true;
	        }
	        else
	        {
		        return false;
	        }
        }

        public bool LinkCameraEx(string sIP, ushort shortCmdPort, ushort shortImgPort, string charCloudUser, string charCloudPwd,
            uint intCamSN, string charCamUser, string charCamPwd, uint intTimeOut)
        {
            return LinkCameraEx(WINAPI.inet_addr(sIP), shortCmdPort, shortImgPort, charCloudUser, charCloudPwd,
                intCamSN, charCamUser, charCamPwd, intTimeOut);
        }

        public bool LinkCameraEx(uint intIP, ushort shortCmdPort, ushort shortImgPort, string charCloudUser, string charCloudPwd,
            uint intCamSN, string charCamUser, string charCamPwd, uint intTimeOut)
        {
            if (GroupSDK.MAG_LinkCameraEx(m_intChannelIndex, intIP, shortCmdPort, shortImgPort, charCloudUser, charCloudPwd,
                intCamSN, charCamUser, charCamPwd, intTimeOut))
            {
                m_intCameraIPAddr = intIP;
                GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
                GroupSDK.MAG_ReadCameraRegContent(m_intChannelIndex, ref m_RegContent, intTimeOut, 0);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisLinkCamera()
        {
            //remember to stop sd storage before dislink
	        if (m_bIsRecordingMgs)
	        {
		        SDStorageMGSStop();
	        }

	        if (m_bIsRecordingAvi)
	        {
		        SDStorageAviStop();
	        }

	        m_intCameraIPAddr = 0;

	        GroupSDK.MAG_DisLinkCamera(m_intChannelIndex);
        }

        public uint GetRecentHeartBeat()
        {
            return GroupSDK.MAG_GetRecentHeartBeat(m_intChannelIndex);
        }

        public bool IsListening()
        {
            return GroupSDK.MAG_IsListening(m_intChannelIndex);
        }

	    public bool ListenTo(uint intIP)
        {
            if (GroupSDK.MAG_ListenTo(m_intChannelIndex, intIP))
	        {
		        GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
		        return true;
	        }
	        else
	        {
		        return false;
	        }
        }

	    public void StopListen()
        {
            GroupSDK.MAG_StopListen(m_intChannelIndex);
        }

        /// <summary>
        /// 重启热像仪
        /// </summary>
        /// <returns></returns>
	    public bool ResetCamera()
        {
            //the user should stop image process before reset
	        //if you forget, the sdk will call MAG_StopProcessImage()

	        //remember to stop sd storage before reset
	        if (m_bIsRecordingMgs)
	        {
		        SDStorageMGSStop();
	        }

	        if (m_bIsRecordingAvi)
	        {
		        SDStorageAviStop();
	        }

	        if (GroupSDK.MAG_ResetCamera(m_intChannelIndex))
	        {
		        //MAG_ResetCamera() will call MAG_Free() and MAG_DelChannel()
		        //so the channel is invalid now
		        m_bIsInitialized = false;
		        m_intChannelIndex = 0xffffffff;

		        //this object is reusable after call Initialize()

		        return true;
	        }
	        else
	        {
		        return false;
	        }
        }

	    public bool TriggerFFC()
        {
            return GroupSDK.MAG_TriggerFFC(m_intChannelIndex);
        }

	    public bool AutoFocus()
        {
            return GroupSDK.MAG_SetPTZCmd(m_intChannelIndex, GroupSDK.PTZIRCMD.PTZFocusAuto, 0);
        }

	    public bool SetPTZCmd(GroupSDK.PTZIRCMD cmd, uint dwPara)
        {
            return GroupSDK.MAG_SetPTZCmd(m_intChannelIndex, cmd, dwPara);
        }

	    public bool QueryPTZState(GroupSDK.PTZQuery query, ref int intValue, uint intTimeout)
        {
            return GroupSDK.MAG_QueryPTZState(m_intChannelIndex, query, ref intValue, intTimeout);
        }

	    public bool SetSerialCmd(byte[] buffer, uint intBufferLen)
        {
            return GroupSDK.MAG_SetSerialCmd(m_intChannelIndex, buffer, intBufferLen);
        }
    	
	    public bool GetCameraTemperature([MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]int[] intT, uint intTimeout)
        {
            return GroupSDK.MAG_GetCameraTemperature(m_intChannelIndex, intT, intTimeout);
        }

	    public bool SetCameraRegContent(GroupSDK.CAMERA_REGCONTENT RegContent)
        {
            if (GroupSDK.MAG_SetCameraRegContent(m_intChannelIndex, ref RegContent))
	        {
		        GroupSDK.MAG_ReadCameraRegContent(m_intChannelIndex, ref m_RegContent, 2 * MAG_DEFAULT_TIMEOUT, 0);
		        return true;
	        }
	        else
	        {
		        return false;
	        }
        }

	    public bool SetUserROIs(GroupSDK.USER_ROI roi)
        {
            return GroupSDK.MAG_SetUserROIs(m_intChannelIndex, ref roi);
        }

	    public bool IsProcessingImage()
        {
            return GroupSDK.MAG_IsProcessingImage(m_intChannelIndex);
        }

	    public bool StartProcessImage(GroupSDK.OUTPUT_PARAM param, GroupSDK.DelegateNewFrame funcFrame, uint dwStreamType, IntPtr pUserData)
        {
            return GroupSDK.MAG_StartProcessImage(m_intChannelIndex, ref param, funcFrame, dwStreamType, pUserData);
        }

	    public bool StartProcessPulseImage(GroupSDK.OUTPUT_PARAM param, GroupSDK.DelegateNewFrame funcFrame, uint dwStreamType, IntPtr pUserData)
        {
            return GroupSDK.MAG_StartProcessPulseImage(m_intChannelIndex, ref param, funcFrame, dwStreamType, pUserData);
        }

	    public bool TransferPulseImage()
        {
            return GroupSDK.MAG_TransferPulseImage(m_intChannelIndex);
        }

	    public void StopProcessImage()
        {
            if (GroupSDK.MAG_IsProcessingImage(m_intChannelIndex))
            {
                GroupSDK.MAG_StopProcessImage(m_intChannelIndex);
            }
        }

	    public void SetColorPalette(GroupSDK.COLOR_PALETTE ColorPaletteIndex)
        {
            GroupSDK.MAG_SetColorPalette(m_intChannelIndex, ColorPaletteIndex);
        }

	    public bool SetSubsectionEnlargePara(int intX1, int intX2, byte byteY1, byte byteY2)
        {
            return GroupSDK.MAG_SetSubsectionEnlargePara(m_intChannelIndex, intX1, intX2, byteY1, byteY2);
        }

	    public void SetAutoEnlargePara(uint dwAutoEnlargeRange, int intBrightOffset, int intContrastOffset)
        {
            GroupSDK.MAG_SetAutoEnlargePara(m_intChannelIndex, dwAutoEnlargeRange, intBrightOffset, intContrastOffset);
        }

	    public void SetEXLevel(GroupSDK.EX ex, int intCenterX, int intCenterY)
        {
            GroupSDK.MAG_SetEXLevel(m_intChannelIndex, ex, intCenterX, intCenterY);
        }

	    public GroupSDK.EX GetEXLevel()
        {
            return GroupSDK.MAG_GetEXLevel(m_intChannelIndex);
        }

	    public void SetDetailEnhancement(int intDDE, int isQuickDDE)
        {
            GroupSDK.MAG_SetDetailEnhancement(m_intChannelIndex, intDDE, isQuickDDE);
        }

	    public bool SetVideoContrast(int intContrastOffset)
        {
            return GroupSDK.MAG_SetVideoContrast(m_intChannelIndex, intContrastOffset);
        }

	    public bool SetVideoBrightness(int intBrightnessOffset)
        {
            return GroupSDK.MAG_SetVideoBrightness(m_intChannelIndex, intBrightnessOffset);
        }

	    public void GetFixPara(ref GroupSDK.FIX_PARAM param)
        {
            GroupSDK.MAG_GetFixPara(m_intChannelIndex, ref param);
        }

        public float SetFixPara(ref GroupSDK.FIX_PARAM param, bool isEnableCameraCorrect)
        {
            return GroupSDK.MAG_SetFixPara(m_intChannelIndex, ref param, isEnableCameraCorrect);
        }

	    public int FixTemperature(int intT, float fEmissivity, uint dwPosX, uint dwPosY)
        {
            return GroupSDK.MAG_FixTemperature(m_intChannelIndex, intT, fEmissivity, dwPosX, dwPosY);
        }

	    public bool GetOutputBMPdata(ref IntPtr pData, ref IntPtr pInfo)
        {
            return GroupSDK.MAG_GetOutputBMPdata(m_intChannelIndex, ref pData, ref pInfo);
        }

	    public bool GetOutputColorBardata(ref IntPtr pData, ref IntPtr pInfo)
        {
            return GroupSDK.MAG_GetOutputColorBardata(m_intChannelIndex, ref pData, ref pInfo);
        }

	    public bool GetOutputVideoData(ref IntPtr pData, ref IntPtr pInfo)
        {
            return GroupSDK.MAG_GetOutputVideoData(m_intChannelIndex, ref pData, ref pInfo);
        }

	    public uint GetDevIPAddress()
        {
            return m_intCameraIPAddr;
        }

        public GroupSDK.TEMP_STATE GetFrameStatisticalData()
        {
            int size = (int)Marshal.SizeOf(typeof(GroupSDK.TEMP_STATE));

            IntPtr ptr = GroupSDK.MAG_GetFrameStatisticalData(m_intChannelIndex);

            GroupSDK.TEMP_STATE state = (GroupSDK.TEMP_STATE)Marshal.PtrToStructure(ptr, typeof(GroupSDK.TEMP_STATE));

            return state;
        }

        public bool GetTemperatureData(int[] data, uint intBufferSize, int isEnableExtCorrect)
        {
            return GroupSDK.MAG_GetTemperatureData(m_intChannelIndex, data, intBufferSize, isEnableExtCorrect);
        }

        public bool GetTemperatureDataRaw(int[] data, uint intBufferSize, int isEnableExtCorrect)
        {
            return GroupSDK.MAG_GetTemperatureData_Raw(m_intChannelIndex, data, intBufferSize, isEnableExtCorrect);
        }

        public int GetTemperatureProbe(uint dwPosX, uint dwPosY, uint intSize)
        {
            return GroupSDK.MAG_GetTemperatureProbe(m_intChannelIndex, dwPosX, dwPosY, intSize);
        }

        public int GetLineTemperatureInfo(int[] buffer, uint intBufferSizeByte, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)]int[] info, uint x0, uint y0, uint x1, uint y1)
        {
            return GroupSDK.MAG_GetLineTemperatureInfo(m_intChannelIndex, buffer, intBufferSizeByte, info, x0, y0, x1, y1);
        }

        public bool GetRectTemperatureInfo(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info)
        {
            return GroupSDK.MAG_GetRectTemperatureInfo(m_intChannelIndex, x0, y0, x1, y1, info);
        }

        public bool GetEllipseTemperatureInfo(uint x0, uint y0, uint x1, uint y1, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info)
        {
            return GroupSDK.MAG_GetEllipseTemperatureInfo(m_intChannelIndex, x0, y0, x1, y1, info);
        }

	    public bool GetRgnTemperatureInfo(uint[] pos, uint intPosNumber, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)]int[] info)
        {
            return GroupSDK.MAG_GetRgnTemperatureInfo(m_intChannelIndex, pos, intPosNumber, info);
        }

        public bool UseTemperatureMask(int isUse)
        {
            return GroupSDK.MAG_UseTemperatureMask(m_intChannelIndex, isUse);
        }

	    public bool IsUsingTemperatureMask()
        {
            return GroupSDK.MAG_IsUsingTemperatureMask(m_intChannelIndex);
        }

        public bool SaveBMP(uint dwIndex, [MarshalAs(UnmanagedType.LPWStr)] string sFileName)
        {
            return GroupSDK.MAG_SaveBMP(m_intChannelIndex, dwIndex, sFileName);
        }

        public bool SaveMGT([MarshalAs(UnmanagedType.LPWStr)] string sFileName)
        {
            return GroupSDK.MAG_SaveMGT(m_intChannelIndex, sFileName);
        }

        public bool SaveDDT([MarshalAs(UnmanagedType.LPWStr)] string sFileName)
        {
            return GroupSDK.MAG_SaveDDT(m_intChannelIndex, sFileName);
        }

        public int SaveDDT2Buffer(byte[] pBuffer, uint intBufferSize)
        {
            return GroupSDK.MAG_SaveDDT2Buffer(m_intChannelIndex, pBuffer, intBufferSize);
        }

        public bool LoadDDT(GroupSDK.OUTPUT_PARAM param, [MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData)
        {             
            if (GroupSDK.MAG_IsLinked(m_intChannelIndex))
            {
                return false;
            }

            if (!GroupSDK.MAG_IsInitialized(m_intChannelIndex) && !GroupSDK.MAG_Initialize(m_intChannelIndex, m_hWndMsg))
            {
                return false;
            }

            if (!GroupSDK.MAG_LoadDDT(m_intChannelIndex, ref param, sFileName, funcFrame, pUserData)){
                return false;
            }

            GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
            return true;
              

        }

        public bool LoadBufferedDDT(GroupSDK.OUTPUT_PARAM param, IntPtr pBuffer, uint size, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData)
        {
            if (!GroupSDK.MAG_IsProcessingImage(m_intChannelIndex))
            {
                if (!GroupSDK.MAG_LoadBufferedDDT(m_intChannelIndex, ref param, pBuffer, size, funcFrame, pUserData))
                {
                    return false;
                }

                GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
                return true;
            }

            return true;
        }

        public bool SetAsyncCompressCallBack(uint intChannelIndex, GroupSDK.DelegateDDTCompressComplete funcDDTCompressComplete, int intQuality)
        {
            return GroupSDK.MAG_SetAsyncCompressCallBack(m_intChannelIndex, funcDDTCompressComplete, intQuality);
        }

        public bool GrabAndAsyncCompressDDT(uint intChannelIndex, IntPtr pUserData)
        {
            return GroupSDK.MAG_GrabAndAsyncCompressDDT(m_intChannelIndex, pUserData);
        }

        public bool LocalStorageAviStart([MarshalAs(UnmanagedType.LPWStr)] string sFileName, uint samplePeriod)
        {
            m_bIsRecordingLocalAvi |= GroupSDK.MAG_LocalStorageAviStart(m_intChannelIndex, sFileName, samplePeriod);
            return m_bIsRecordingLocalAvi;
        }

        public void LocalStorageAviStop()
        {
            GroupSDK.MAG_LocalStorageAviStop(m_intChannelIndex);
            m_bIsRecordingLocalAvi = false;
        }

        public bool LocalStorageMgsRecord([MarshalAs(UnmanagedType.LPWStr)] string sFileName, uint samplePeriod)
        {
            m_bIsRecordingLocalMgs |= GroupSDK.MAG_LocalStorageMgsRecord(m_intChannelIndex, sFileName, samplePeriod);
            return m_bIsRecordingLocalMgs;
        }

        public int LocalStorageMgsPlay([MarshalAs(UnmanagedType.LPWStr)] string sFileName, GroupSDK.DelegateNewFrame funcFrame, IntPtr pUserData)
        {
            int totalFrames = GroupSDK.MAG_LocalStorageMgsPlay(m_intChannelIndex, sFileName, funcFrame, pUserData);
            if (totalFrames > 0)
            {
                m_bIsPlayingLocalMgs = true;
            }

            if (m_bIsPlayingLocalMgs)
            {
                GroupSDK.MAG_GetCamInfo(m_intChannelIndex, ref m_CamInfo, Marshal.SizeOf(typeof(GroupSDK.CAMERA_INFO)));
            }

            return totalFrames;
        }

        public bool LocalStorageMgsSeekFrame(uint hFrame)
        {
            return GroupSDK.MAG_LocalStorageMgsSeekFrame(m_intChannelIndex, hFrame);
        }

        public bool LocalStorageMgsPopFrame()
        {
            return GroupSDK.MAG_LocalStorageMgsPopFrame(m_intChannelIndex);
        }

        public void LocalStorageMgsStop()
        {
            GroupSDK.MAG_LocalStorageMgsStop(m_intChannelIndex);
            m_bIsRecordingLocalMgs = false;
            m_bIsPlayingLocalMgs = false;
        }

        public bool isPlayingLocalMgs()
        {
            return m_bIsPlayingLocalMgs;
        }

        public bool SDStorageMGT()
        {
            return GroupSDK.MAG_SDStorageMGT(m_intChannelIndex);
        }

        public bool SDStorageBMP()
        {
            return GroupSDK.MAG_SDStorageBMP(m_intChannelIndex);
        }

        public bool SDStorageMGSStart()
        {
            m_bIsRecordingMgs |= GroupSDK.MAG_SDStorageMGSStart(m_intChannelIndex);
            return m_bIsRecordingMgs;
        }

        public bool SDStorageMGSStop()
        {
            bool bReturn = GroupSDK.MAG_SDStorageMGSStop(m_intChannelIndex);

            if (bReturn)
            {
                m_bIsRecordingMgs = false;
            }

            return bReturn;
        }

        public bool SDStorageAviStart()
        {
            m_bIsRecordingAvi |= GroupSDK.MAG_SDStorageAviStart(m_intChannelIndex);
            return m_bIsRecordingAvi;
        }

	    public bool SDStorageAviStop()
        {
            bool bReturn = GroupSDK.MAG_SDStorageAviStop(m_intChannelIndex);

            if (bReturn)
            {
                m_bIsRecordingAvi = false;
            }

            return bReturn;
        }

        public bool SDCardStorage(uint hDevice, GroupSDK.SDStorageFileType filetype, uint para)
        {
            return GroupSDK.MAG_SDCardStorage(m_intChannelIndex, filetype, para);
        }

	    public bool GetCurrentOffset([MarshalAs(UnmanagedType.LPWStr)] string sReferenceDDT, ref int offsetx, ref int offsety)
        {
            return GroupSDK.MAG_GetCurrentOffset(m_intChannelIndex, sReferenceDDT, ref offsetx, ref offsety);
        }

        public void Lock()
        {
            GroupSDK.MAG_LockFrame(m_intChannelIndex);
        }

	    public void Unlock()
        {
            GroupSDK.MAG_UnLockFrame(m_intChannelIndex);
        }

        public void SetIsothermalPara(int intLowerLimit, int intUpperLimit)
        {
            GroupSDK.MAG_SetIsothermalPara(m_intChannelIndex, intLowerLimit, intUpperLimit);
        }

        public bool SetSerialCallBack(GroupSDK.DelegateSerial cb, IntPtr pUserData)
        {
            return GroupSDK.MAG_SetSerialCallBack(m_intChannelIndex, cb, pUserData);
        }

        public bool SetReConnectCallBack(GroupSDK.DelegateReconnect cb, IntPtr pUserData)
        {
            return GroupSDK.MAG_SetReConnectCallBack(m_intChannelIndex, cb, pUserData);
        }
    }
}
