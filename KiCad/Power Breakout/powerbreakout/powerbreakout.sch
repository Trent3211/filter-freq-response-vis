EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr A4 11693 8268
encoding utf-8
Sheet 1 1
Title ""
Date ""
Rev ""
Comp ""
Comment1 ""
Comment2 ""
Comment3 ""
Comment4 ""
$EndDescr
$Comp
L ffrv_power:P7805-Q12-S2-S__2.5V U1
U 1 1 63F49714
P 5450 3450
F 0 "U1" H 5450 3765 50  0000 C CNN
F 1 "P7805-Q12-S2-S__2.5V" H 5450 3674 50  0000 C CNN
F 2 "Package_SIP:SIP3_11.6x8.5mm" H 5450 3750 50  0001 C CNN
F 3 "" H 5450 3750 50  0001 C CNN
	1    5450 3450
	1    0    0    -1  
$EndComp
$Comp
L ffrv_power:V7805-500__5V U3
U 1 1 63F49CE4
P 7100 3450
F 0 "U3" H 7100 3765 50  0000 C CNN
F 1 "V7805-500__5V" H 7100 3674 50  0000 C CNN
F 2 "Package_SIP:SIP3_11.6x8.5mm" H 7100 3750 50  0001 C CNN
F 3 "" H 7100 3750 50  0001 C CNN
	1    7100 3450
	1    0    0    -1  
$EndComp
$Comp
L ffrv_power:P7805-Q12-S2-S__2.5V U2
U 1 1 63F4A69B
P 5450 5050
F 0 "U2" H 5450 5365 50  0000 C CNN
F 1 "P7805-Q12-S2-S__2.5V" H 5450 5274 50  0000 C CNN
F 2 "Package_SIP:SIP3_11.6x8.5mm" H 5450 5350 50  0001 C CNN
F 3 "" H 5450 5350 50  0001 C CNN
	1    5450 5050
	1    0    0    -1  
$EndComp
$Comp
L ffrv_power:V7805-500__5V U4
U 1 1 63F4A6A1
P 7100 5050
F 0 "U4" H 7100 5365 50  0000 C CNN
F 1 "V7805-500__5V" H 7100 5274 50  0000 C CNN
F 2 "Package_SIP:SIP3_11.6x8.5mm" H 7100 5350 50  0001 C CNN
F 3 "" H 7100 5350 50  0001 C CNN
	1    7100 5050
	1    0    0    -1  
$EndComp
Wire Wire Line
	5050 3450 4900 3450
Wire Wire Line
	4500 4050 3600 4050
Wire Wire Line
	4500 5050 4900 5050
Connection ~ 4500 4050
Wire Wire Line
	3600 4250 3750 4250
Wire Wire Line
	4500 3450 4500 4050
Wire Wire Line
	4500 4050 4500 5050
Wire Wire Line
	5850 5050 6150 5050
Wire Wire Line
	6150 5050 6150 4250
Connection ~ 6150 4250
Wire Wire Line
	7850 4250 7850 5050
Wire Wire Line
	7850 5050 7750 5050
Wire Wire Line
	6150 4250 6650 4250
Wire Wire Line
	7100 3700 7100 4250
Connection ~ 7100 4250
Wire Wire Line
	7100 4250 7850 4250
Wire Wire Line
	5450 3700 5450 4250
Connection ~ 5450 4250
Wire Wire Line
	5450 4250 6150 4250
$Comp
L Device:CP C1
U 1 1 63F4DA4F
P 4900 3850
F 0 "C1" H 5018 3896 50  0000 L CNN
F 1 "10u" H 5018 3805 50  0000 L CNN
F 2 "Capacitor_THT:C_Radial_D6.3mm_H7.0mm_P2.50mm" H 4938 3700 50  0001 C CNN
F 3 "~" H 4900 3850 50  0001 C CNN
	1    4900 3850
	1    0    0    -1  
$EndComp
Wire Wire Line
	4900 3700 4900 3450
Connection ~ 4900 3450
Wire Wire Line
	4900 3450 4500 3450
Wire Wire Line
	4900 4000 4900 4250
Connection ~ 4900 4250
Wire Wire Line
	4900 4250 5450 4250
$Comp
L Device:CP C2
U 1 1 63F4E94E
P 4900 4650
F 0 "C2" H 4782 4604 50  0000 R CNN
F 1 "10u" H 4782 4695 50  0000 R CNN
F 2 "Capacitor_THT:C_Radial_D6.3mm_H7.0mm_P2.50mm" H 4938 4500 50  0001 C CNN
F 3 "~" H 4900 4650 50  0001 C CNN
	1    4900 4650
	-1   0    0    1   
$EndComp
Wire Wire Line
	4900 4500 4900 4250
Wire Wire Line
	4900 4800 4900 5050
Connection ~ 4900 5050
Wire Wire Line
	4900 5050 5050 5050
$Comp
L Device:CP C3
U 1 1 63F4F8D9
P 6150 5400
F 0 "C3" H 6268 5446 50  0000 L CNN
F 1 "10u" H 6268 5355 50  0000 L CNN
F 2 "Capacitor_THT:C_Radial_D6.3mm_H7.0mm_P2.50mm" H 6188 5250 50  0001 C CNN
F 3 "~" H 6150 5400 50  0001 C CNN
	1    6150 5400
	1    0    0    -1  
$EndComp
Wire Wire Line
	6150 5050 6150 5250
Connection ~ 6150 5050
Wire Wire Line
	5450 5300 5450 5700
Wire Wire Line
	5450 5700 6150 5700
Wire Wire Line
	6150 5700 6150 5550
Wire Wire Line
	7100 5300 7100 5700
Wire Wire Line
	6150 5700 6500 5700
Connection ~ 6150 5700
Wire Wire Line
	5850 3450 6150 3450
$Comp
L Device:CP C6
U 1 1 63F532AE
P 7750 5400
F 0 "C6" H 7868 5446 50  0000 L CNN
F 1 "10u" H 7868 5355 50  0000 L CNN
F 2 "Capacitor_THT:C_Radial_D6.3mm_H7.0mm_P2.50mm" H 7788 5250 50  0001 C CNN
F 3 "~" H 7750 5400 50  0001 C CNN
	1    7750 5400
	1    0    0    -1  
$EndComp
Wire Wire Line
	7750 5700 7750 5550
Wire Wire Line
	7750 5250 7750 5050
Connection ~ 7750 5050
Wire Wire Line
	7750 5050 7500 5050
$Comp
L Device:CP C4
U 1 1 63F541CB
P 6650 3900
F 0 "C4" H 6768 3946 50  0000 L CNN
F 1 "10u" H 6768 3855 50  0000 L CNN
F 2 "Capacitor_THT:C_Radial_D6.3mm_H7.0mm_P2.50mm" H 6688 3750 50  0001 C CNN
F 3 "~" H 6650 3900 50  0001 C CNN
	1    6650 3900
	1    0    0    -1  
$EndComp
$Comp
L Device:CP C5
U 1 1 63F54E8B
P 6650 4650
F 0 "C5" H 6532 4604 50  0000 R CNN
F 1 "10u" H 6532 4695 50  0000 R CNN
F 2 "Capacitor_THT:C_Radial_D6.3mm_H7.0mm_P2.50mm" H 6688 4500 50  0001 C CNN
F 3 "~" H 6650 4650 50  0001 C CNN
	1    6650 4650
	-1   0    0    1   
$EndComp
Wire Wire Line
	6650 4800 6650 5050
Connection ~ 6650 5050
Wire Wire Line
	6650 5050 6700 5050
Wire Wire Line
	6650 4500 6650 4250
Connection ~ 6650 4250
Wire Wire Line
	6650 4250 7100 4250
Wire Wire Line
	6650 4050 6650 4250
Wire Wire Line
	6650 3750 6650 3450
Connection ~ 6650 3450
Wire Wire Line
	6650 3450 6700 3450
Wire Wire Line
	6350 5050 6350 4050
Wire Wire Line
	6350 3450 6650 3450
Wire Wire Line
	6350 5050 6650 5050
Wire Wire Line
	4500 4050 6350 4050
Connection ~ 6350 4050
Wire Wire Line
	6350 4050 6350 3450
$Comp
L Connector_Generic:Conn_01x04 J2
U 1 1 63F5D1A4
P 9000 3250
F 0 "J2" H 9080 3242 50  0000 L CNN
F 1 "Conn_01x04" H 9080 3151 50  0000 L CNN
F 2 "Connector_PinSocket_2.54mm:PinSocket_1x04_P2.54mm_Vertical" H 9000 3250 50  0001 C CNN
F 3 "~" H 9000 3250 50  0001 C CNN
	1    9000 3250
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x04 J4
U 1 1 63F5E3E8
P 9000 5800
F 0 "J4" H 9080 5792 50  0000 L CNN
F 1 "Conn_01x04" H 9080 5701 50  0000 L CNN
F 2 "Connector_PinSocket_2.54mm:PinSocket_1x04_P2.54mm_Vertical" H 9000 5800 50  0001 C CNN
F 3 "~" H 9000 5800 50  0001 C CNN
	1    9000 5800
	1    0    0    -1  
$EndComp
Wire Wire Line
	6150 3150 6150 3450
Wire Wire Line
	6500 5700 6500 6000
Wire Wire Line
	6500 6000 8250 6000
Wire Wire Line
	6150 3150 8700 3150
$Comp
L Connector_Generic:Conn_01x04 J3
U 1 1 63F5BBF6
P 9000 4350
F 0 "J3" H 9080 4342 50  0000 L CNN
F 1 "Conn_01x04" H 9080 4251 50  0000 L CNN
F 2 "Connector_PinSocket_2.54mm:PinSocket_1x04_P2.54mm_Vertical" H 9000 4350 50  0001 C CNN
F 3 "~" H 9000 4350 50  0001 C CNN
	1    9000 4350
	1    0    0    -1  
$EndComp
Connection ~ 7850 4250
Wire Wire Line
	7500 3450 8350 3450
Wire Wire Line
	8800 3350 8700 3350
Wire Wire Line
	8250 3350 8250 6000
Wire Wire Line
	8350 3450 8350 5700
Wire Wire Line
	8350 5700 8700 5700
Wire Wire Line
	7850 4250 8700 4250
Wire Wire Line
	8700 4250 8700 4350
Wire Wire Line
	8700 4550 8800 4550
Connection ~ 8700 4250
Wire Wire Line
	8700 4250 8800 4250
Wire Wire Line
	8800 4450 8700 4450
Connection ~ 8700 4450
Wire Wire Line
	8700 4450 8700 4550
Wire Wire Line
	8800 4350 8700 4350
Connection ~ 8700 4350
Wire Wire Line
	8700 4350 8700 4450
Wire Wire Line
	8800 3450 8700 3450
Wire Wire Line
	8700 3450 8700 3350
Connection ~ 8700 3350
Wire Wire Line
	8700 3350 8250 3350
Wire Wire Line
	8800 3250 8700 3250
Wire Wire Line
	8700 3250 8700 3150
Connection ~ 8700 3150
Wire Wire Line
	8700 3150 8800 3150
Wire Wire Line
	7100 5700 7750 5700
Wire Wire Line
	7750 5700 8000 5700
Wire Wire Line
	8000 5700 8000 5900
Wire Wire Line
	8000 5900 8700 5900
Connection ~ 7750 5700
Wire Wire Line
	8700 5700 8700 5800
Wire Wire Line
	8700 5800 8800 5800
Connection ~ 8700 5700
Wire Wire Line
	8700 5700 8800 5700
Wire Wire Line
	8700 5900 8700 6000
Wire Wire Line
	8700 6000 8800 6000
Connection ~ 8700 5900
Wire Wire Line
	8700 5900 8800 5900
Text Label 7300 4250 0    50   ~ 0
GND
Text Label 7700 3450 0    50   ~ 0
+5V
Text Label 7700 3150 0    50   ~ 0
+2.5V
Text Label 7500 6000 0    50   ~ 0
-2.5V
Text Label 7500 5700 0    50   ~ 0
-5V
Text Label 4150 4050 0    50   ~ 0
+12V
$Comp
L Connector:Barrel_Jack_Switch J1
U 1 1 63F8E489
P 3300 4150
F 0 "J1" H 3357 4467 50  0000 C CNN
F 1 "Barrel_Jack_Switch" H 3357 4376 50  0000 C CNN
F 2 "Connector_BarrelJack:BarrelJack_Horizontal" H 3350 4110 50  0001 C CNN
F 3 "~" H 3350 4110 50  0001 C CNN
	1    3300 4150
	1    0    0    -1  
$EndComp
Wire Wire Line
	3600 4150 3750 4150
Wire Wire Line
	3750 4150 3750 4250
Connection ~ 3750 4250
Wire Wire Line
	3750 4250 4900 4250
$Comp
L Mechanical:MountingHole H1
U 1 1 63F9C0D5
P 4000 2600
F 0 "H1" H 4100 2646 50  0000 L CNN
F 1 "MountingHole" H 4100 2555 50  0000 L CNN
F 2 "MountingHole:MountingHole_2.5mm" H 4000 2600 50  0001 C CNN
F 3 "~" H 4000 2600 50  0001 C CNN
	1    4000 2600
	1    0    0    -1  
$EndComp
$Comp
L Mechanical:MountingHole H2
U 1 1 63F9C43B
P 4000 2950
F 0 "H2" H 4100 2996 50  0000 L CNN
F 1 "MountingHole" H 4100 2905 50  0000 L CNN
F 2 "MountingHole:MountingHole_2.5mm" H 4000 2950 50  0001 C CNN
F 3 "~" H 4000 2950 50  0001 C CNN
	1    4000 2950
	1    0    0    -1  
$EndComp
$EndSCHEMATC
