
Global Const $iTime = 500
Global $TimeInit = TimerInit()
while 1
	If TimerDiff($TimeInit) > 5000 Then
		Send("{ScrollLock ON}")
		Send("{NUMLOCK ON}")

		Sleep(1000)
		Send("{NUMLOCK OFF}")
		$TimeInit = TimerInit()
	Else
		Send("{ScrollLock ON}")
		Sleep($iTime)
		Send("{ScrollLock OFF}")
		Sleep($iTime)
	EndIf
WEnd