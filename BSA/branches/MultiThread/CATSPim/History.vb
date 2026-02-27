Module History
	'
	'*** ver1.0.2.0 2017/2/4
	'- Add vibration function

	'*** ver1.0.2.1 2017/2/6
	'- Get Ret p/n & s/n from SingleRet_DeviceData/MultiRet_DeviceData ,NOT from GetInfo
	'
	'*** ver1.0.2.2 2017/2/10
	'- Modify write_head function , tilt from single to string
	'
	'*** ver1.0.3.0 2017/4/1
	'- Read local system config c:\cats\test_system\cats.xmls
	'
	'*** ver1.0.3.1 2017/4/4
	'- For 2-tone testing, in the result file trace x2-x4 only write 1 point
	'
	'** ver1.0.3.2 2017/4/7
	'- Reference the latest RFInterface.dll which add Computer Name, UserName and Mode in interface
	'- Cancel write trace x4 for sweep up & sweep down in result file.
End Module
