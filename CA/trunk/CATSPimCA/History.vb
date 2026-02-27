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
    '- get correct phase status by stored procedured 'proc_cq_meas_phases_status_v2'


    '** ver1.0.5.5  2017/8/23
    '- Change RFPAInterface -- update the layout and font properties.


    '** ver1.0.5.6  2017/8/24
    '- Fixed a bug -- check the product whether support test station like pre-test or final-test ...

    '** ver1.1.2.1  2018/1/29
    '- Add dynamic fixture control function, add retrieve cable assembly information thru SAP web request function
    '  disable check test cable usage counts function

    '** ver1.1.2.6  2018/3/12
    '- Add STS-C test flow

    '** ver1.1.3.1  2018/4/20
    '- Add supporting multi mode

    '** ver1.1.3.2  2018/4/23
    '- Add supporting SAP Fail Safe Mode

    '** ver1.1.3.3  2018/5/24
    '- Add GRR Measuremnent

    '** ver1.1.3.4  2018/6/13
    '- Add Dynamic fixture control board SeaDAC Lite 8112

    '*** version 1.0.0.10 2018/9/5
    '- Add supporting Zulu unit function

    '*** version 1.0.0.15 2018/11/06
    '- Add Connection message before testing

    '*** version 1.0.0.18 2019/06/13
    '- Change test count from 3 to 2
    '- Resume limit of up to 3 test times

    '*** version 1.0.0.19 2019/06/19
    '- Change test count from 3 to 1

    '*** version 1.0.0.23 2019/09/29
    '- Add STS-B test flow

    '*** version 1.0.0.24 2019/10/09
    '- Fix the bug of STS-B test flow
End Module
