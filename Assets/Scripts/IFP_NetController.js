#pragma strict

private var	m_tpNetworkClient : TPNetworkClient = null;
private var m_connection : String = "";
private var m_tpSharedInfo : TPSharedInfo = null;
private var m_sharedValue : String = "";

var autoConnect : boolean = false;
private var autoConnectBak : boolean;

function Awake () {
	autoConnectBak = autoConnect;
}

function Start () {
	if( PlayerPrefs.GetInt("auto_connect") == 1 )
		autoConnect = true;
	else
		autoConnect = false;
}

//没用上
function initNetPrms() {
	autoConnect = autoConnectBak;
}

//没有用上
function sendMessage (mstr : String) {
	if (isConnectNetwork() == false)
		return;
	if (mstr != "") {
		if (m_tpNetworkClient != null)
			m_tpNetworkClient.Send(mstr);
	}
}

function conectNetwork() {
	if (m_connection == "Connected")
		return;

	m_tpNetworkClient = new TPNetworkClient();
	if (m_tpNetworkClient.Initialize()) {
		m_tpSharedInfo = new TPSharedInfo();
		var connected : boolean = false;
			connected = m_tpSharedInfo.Initialize(m_tpNetworkClient.GetIPAddress(),
		                    m_tpNetworkClient.GetPort() + 1);
			m_connection = connected ? "Connected" : "Disconnected";
	}
	else
		m_connection = "Disconnected";
}

function disconectNetwork() {
/****
	if (m_connection == "Disconnected") {
		m_tpNetworkClient = null;
		m_tpSharedInfo = null;
		m_sharedValue = "";
		return;
	}
****/
	if (m_tpNetworkClient != null) {
		m_tpNetworkClient.Terminate();
		m_tpNetworkClient = null;
	}
	if (m_tpSharedInfo != null) {
		m_tpSharedInfo.Terminate();
		m_tpSharedInfo = null;
		m_sharedValue = "";
	}
	m_connection = "Disconnected";
}

function resetNetwork()
{
	disconectNetwork();
	conectNetwork();
}

function isConnectNetwork()
{
	if (m_tpNetworkClient == null) {
		m_connection = "Disconnected";
		return false;
	}
	if (m_tpSharedInfo == null) {
		m_connection = "Disconnected";
		return false;
	}

	m_connection = m_tpSharedInfo.IsConnected() ? "Connected" : "Disconnected";
	if (m_connection == "Connected") {
		return true;
	}
	else {
		return false;
	}
}

function GetAutoConnect() {
	return autoConnect;
}

function SetAutoConnect(stat : boolean) {
	autoConnect = stat;
}

/***************************/
function GetTPSharedInfo() {
	return m_tpSharedInfo;
}

function TPSInfoCheckControlEvents() {
	if (m_tpSharedInfo == null || m_connection == "Disconnected")
		return 0;
	return m_tpSharedInfo.CheckControlEvents();
}

function TPSInfoGetControlEvent(i : int) {
	if (m_tpSharedInfo == null || m_connection == "Disconnected")
		return null;
	return m_tpSharedInfo.GetControlEvent(i);
}

function TPSInfoResetControlEvent() {
	if (m_tpSharedInfo == null || m_connection == "Disconnected")
		return;
	m_tpSharedInfo.ResetControlEvent();
}

    /***************************/

//自动连接的出口
function Update () {
	if (autoConnect) {
		if (isConnectNetwork() == false)
			conectNetwork();
	}
}

//在编辑模式下也运行脚本
@script ExecuteInEditMode

