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
    '
    '** ver1.0.3.3 2017/4/7
    '- Cancel stop test when test fail.
    '
    '** ver1.0.3.4 2017/4/21
    '- Support STS-J (only sweep up test).

    '** ver1.0.3.5 2017/4/25
    '- Re layout the test interface
    '- Set to min tilt when end test

    '** ver1.0.3.6 2017/5/3
    '- Support multiple suppliers of test instrument

    '** ver1.0.3.7 2017/5/4
    '- Support multiple model of same supplier and same frequency instrument

    '** ver1.0.3.8 2017/5/5
    '- Change tx and rx frequencys to decimal

    '** ver1.0.3.9 2017/5/10
    '- Read summitek instrument model and serial number , then write into result file.

    '** ver1.0.4.0 2017/5/11
    '- Support vibration equipment been driven by JSB336 board 


    '** ver1.0.4.1 2017/5/17
    '- To support Kaelus equipment testing


    '** ver1.0.4.2 2017/5/18
    '- To support cRET antenna

    'SAMPLE
    ' 1> *************** Antenna: EGYHHTT-65A-R6 Barcode:17CN101557083
    'OLD Firmware
    'SeiralNumber					Addr     AISGVer       Firmware version         Antenna Model				DeviceType			Min/Max/Curr Tilt
    'CP0017CN101557083t4    1           AISG 2.0      001.002.005                EGYHHTT-65A-R6         Single-RET         2/12/2
    'CP0017CN101557083t3    2           AISG 2.0      001.002.005                EGYHHTT-65A-R6         Single-RET         2/12/2
    'CP0017CN101557083t2    3           AISG 2.0      001.002.005                EGYHHTT-65A-R6         Single-RET         2/17/2
    'CP0017CN101557083t6    4           AISG 2.0      001.002.005                EGYHHTT-65A-R6         Single-RET         2/12/2
    'CP0017CN101557083t1    5           AISG 2.0      001.002.005                EGYHHTT-65A-R6         Single-RET         2/17/2
    'CP0017CN101557083t5    6           AISG 2.0      001.002.005                EGYHHTT-65A-R6         Single-RET         2/12/2

    'NEW Firmware
    'SeiralNumber                   Addr     AISGVer       Firmware version          Antenna Model         DeviceType      Min/Max/Curr Tilt      
    'CP0017CN101557083Y1    1           AISG 2.0       001.002.006                EGYHHTT-65A-R6         Single-RET         2/12/2
    'CP0017CN101557083G1    2           AISG 2.0      001.002.006                EGYHHTT-65A-R6         Single-RET         2/12/2
    'CP0017CN101557083B2    3           AISG 2.0      001.002.006                EGYHHTT-65A-R6         Single-RET         2/12/2
    'CP0017CN101557083R2    4           AISG 2.0      001.002.006                EGYHHTT-65A-R6         Single-RET         2/17/2
    'CP0017CN101557083B1    5           AISG 2.0      001.002.006                EGYHHTT-65A-R6         Single-RET         2/12/2
    'CP0017CN101557083R1    6           AISG 2.0      001.002.006                EGYHHTT-65A-R6         Single-RET         2/17/2
    '
    ' 2> ************* Antenna: RV4-65D-R5 Barcode: 17TEST0518001
    'SerialNumber						Antenna Model
    'CP017TEST05180011t1		RV4-65D-R5-123
    'CP017TEST05180011t2		RV4-65D-R5-123
    'CP017TEST05180011t3		RV4-65D-R5-123
    'CP017TEST05180012t1		RV4-65D-R5-45
    'CP017TEST05180012t2		RV4-65D-R5-45


    '** ver1.0.4.3 2017/5/24
    '- Add TestModules.FindRetId() Function
    '- Add Jsb336.dll driver

    '** ver1.0.4.4 2017/6/12
    '- Add STS-A test support


    '** ver1.0.4.5 2017/6/27
    '- Fix the bug in ScanAntennaRet() for cRet programming.  
    '       ***********************************************************************************************
    '		rpId = tmpRet.AntennaSn.ToUpper.Trim.IndexOf(AntBarcode) + AntBarcode.Length
    '		band_id = tmpRet.RetSn.Substring(rpId) change to => band_id = tmpRet.AntennaSn.Substring(rpId)
    '       ***********************************************************************************************

    '** ver1.0.4.6 2017/6/28
    '- Fix the bug in ScanAntennaRet() for cRet programming.  
    '       ***********************************************************************************************
    '		change to =>
    '		rpId = tmpRet.RetSn.ToUpper.Trim.IndexOf(AntBarcode) + AntBarcode.Length
    '		band_id = tmpRet.RetSn.Substring(rpId) 
    '       ***********************************************************************************************

    '** ver1.0.4.7 2017/6/30
    '- The Rets return to any been configured angles (spec_main.gen1 [ret idx],spec_main.gen2 [ret angles]  

    '** ver1.0.4.8 2017/7/4
    '- The Rets return to any been configured angles (product_main.gen1 [ret idx],spec_main.gen2 [ret angles]


    '** ver1.0.4.9 2017/7/11
    '- Support special production (like RVVPX303.6F12R2), low band has no RET.

    '** ver1.0.5.0 2017/7/25
    '- Add PreTest function.
    '- Start/Stop vibration when test fail in FormTest . 

    '** ver1.0.5.1 2017/7/27
    '- Cancel two tone test in PreTest function

    '** ver1.0.5.2 2017/8/1
    '- Test cable record


    '** ver1.0.5.3 2017/8/4
    '- Add RET & Vibration debug tool


    '** ver1.0.5.4  2017/8/22
    '- Issue: can not get correct pre-test status in CHECK_SN() function
    '		  get correct phase status by stored procedured 'proc_cq_meas_phases_status_v2'


    '** ver1.0.5.5  2017/8/23
    '- Change RFPAInterface -- update the layout and font properties.


    '** ver1.0.5.6  2017/8/24
    '- Fixed a bug -- check the product whether support test station like pre-test or final-test ...

    '** ver1.0.5.7  2017/9/12
    '- Fixed a bug -- Not display chart when testing STS-A flow 

    '** ver1.0.5.8  2017/9/13
    '- Calibrate standard deviation when testing STS-A ( the IMD value will be set to -160 when it < -160) 

    '** ver1.0.6  2017/9/12
    '- Add MiiFactory

    '** ver1.0.7.0 2017/9/20
    '- Add MiiFactory Pre-Test supporting

    '** ver1.0.8.0  2017/9/21
    '- Support cRET multiple-RET
    '- Fixed a bug on MIIBridge.dll for support pre-test


    '** ver1.0.9.0  2017/9/26
    '- Show alarm when test station is not 'FinalTest'

    '** ver1.0.10.0 2017/10/19
    '- Fix a bug , clear RET parts information before run phase testing 
    '					( found a bug - still write RET parts info when run pre-test testing , because the variable m_RETlist is not clear)

    '** ver1.0.11.0 2017/10/23
    '- After SetTilt(degree) , compare degree to GetTilt() 
    '- Show RET status in FormRetController .

    '** ver1.0.12.0 2017/10/27
    ' Show a prompt when moving RET

    '** ver1.0.13.0 2017/10/30
    ' cRETv2 don't set to shipment tilt
    ' fail the test if AISG cable is unplaged during testing

    '** ver1.0.14.0 2017/11/3
    ' set limit to +/-0.3 when SetTilt() { degree compare with GetTilt())

    '** ver1.0.15.0 2017/11/7
    ' support knocking in testing 

    '** ver1.0.16.0 2017/11/7
    ' support beam_master antenna testing 

    '** ver1.0.17.0 2017/11/10
    ' fixed a bug - cable recorder class have not been initialized before running debug form , 

    '** ver1.0.18.0 2017/11/15
    ' fixed a bug when move default tilts when finish testing
    ' re-scan tilts in item testing if the communication is break (have some bugs)

    '** ver1.0.19.0 2017/11/21
    ' re-scan tilts in item testing if the aisg communication is break (it's ok)

    '** ver1.0.20 2017/12/20
    ' show alarm form in GOA location if the mode is not PROD or test station is not FinalTest

    '** ver1.0.21 2017/12/28
    ' fixed the bug of ver 1.0.20 (only show test station alarm even test mode is not 'PROD')


    '** ver1.0.22 2018/1/12
    ' add 'TestMisc' element in result file , and combine mii pre-check / post-update to 'online' enable

    '** ver1.0.23 2018/1/29
    ' support PIM700LU testing
    ' cancel vibrate when running pre-testing

    '** ver1.0.24 2018/3/6
    ' add tx power detection  -done
    ' No vibration for PRE_TEST (any mode)  -done
    ' PRE_TEST test time reduction – 5 frequency test only
    ' support NOKIA antenna testing , cancel tilt ANT SN checking , such as H16-95B-R2 

    '** ver1.0.25 2018/3/30
    ' support summitek series A instruments(SI-A)

    '** ver1.0.26 2018/4/17
    ' add CalibrationRecord funciton 
    '1) add CalibrationRecord.vb
    '2) add check_Cal in Testplan.vb
    '3) add Write_Cal in TestModeles.vb

    '** ver1.0.29 2018/4/17
    'add chamber monnitor on/off
    'add Joincom PIM device
    'add SN generate function for calibration

    '** ver1.0.30 2018/7/10
    'add chamber door monnitor funciton , 目前函数体部分ok，Test module ->InitSeaDACDevice 可以找到全部的功能，激活后可以使用
    ' 开始测试时功能OK, 测试进行中还需要考虑是否添加和如何添加，add power sensor for power calibration

    '** ver1.0.31 2018/8/20
    'H16/H8先测试所有小角度，再测试大角度，预测部分需要更新下代码
    ' 
    '** ver1.0.32 2018/8/20
    'add power meter for power calibration for GOA and RY.

    '** ver1.0.33 2018/8/20
    'add ZULU

    '** ver1.0.34 2018/8/20
    'add SAP check

    '** ver1.0.35 2018/8/20
    'add Sealevel8222

End Module
