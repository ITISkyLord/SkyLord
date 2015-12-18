(function (lib, img, cjs, ss) {

var p; // shortcut to reference prototypes

// library properties:
lib.properties = {
	width: 550,
	height: 400,
	fps: 24,
	color: "#FFFFFF",
	manifest: []
};



// symbols:



(lib.Nuage1 = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// Calque 1
	this.shape = new cjs.Shape();
	this.shape.graphics.f("#E8E5D9").s().p("AgBAjQgHgBgKgQQgKgQADgEQAEgKAJgLQAMgOAEAFQAEAEAEAKQADAKAEACQAJAFgCAQQgCAUgYAAIgBAAg");
	this.shape.setTransform(3.5,2.7,1,1,90.8);

	this.timeline.addTween(cjs.Tween.get(this.shape).wait(40).to({scaleX:2.63,scaleY:2.63,x:12.4,y:-29.8},0).wait(1));

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(0,0,7.1,5.4);


// stage content:



(lib.Tour = function(mode,startPosition,loop) {
	this.initialize(mode,startPosition,loop,{});

	// Fumée
	this.instance = new lib.Nuage1();
	this.instance.setTransform(281.3,210.7);

	this.timeline.addTween(cjs.Tween.get(this.instance).wait(1).to({regX:10.9,regY:-15.8,scaleX:1.18,scaleY:1.18,rotation:1.2,x:294.6,y:188.9},0).wait(1).to({scaleX:1.3,scaleY:1.3,rotation:2,x:296.3,y:185.1},0).wait(1).to({scaleX:1.39,scaleY:1.39,rotation:2.6,x:297.6,y:182.1},0).wait(1).to({scaleX:1.49,scaleY:1.49,rotation:3.2,x:298.9,y:179.1},0).wait(1).to({scaleX:1.58,scaleY:1.58,rotation:3.8,x:300.3,y:176.3},0).wait(1).to({scaleX:1.67,scaleY:1.67,rotation:4.4,x:301.7,y:173.4},0).wait(1).to({scaleX:1.76,scaleY:1.76,rotation:5,x:303.1,y:170.6},0).wait(1).to({scaleX:1.85,scaleY:1.85,rotation:5.6,x:304.5,y:167.9},0).wait(1).to({scaleX:1.93,scaleY:1.93,rotation:6.1,x:305.9,y:165.3},0).wait(1).to({scaleX:2.02,scaleY:2.02,rotation:6.7,x:307.3,y:162.7},0).wait(1).to({scaleX:2.1,scaleY:2.1,rotation:7.2,x:308.8,y:160.2},0).wait(1).to({scaleX:2.19,scaleY:2.19,rotation:7.8,x:310.1,y:157.7},0).wait(1).to({scaleX:2.27,scaleY:2.27,rotation:8.3,x:311.5,y:155.3},0).wait(1).to({scaleX:2.35,scaleY:2.35,rotation:8.9,x:312.9,y:152.9},0).wait(1).to({scaleX:2.43,scaleY:2.43,rotation:9.4,x:314.3,y:150.6},0).wait(1).to({scaleX:2.51,scaleY:2.51,rotation:9.9,x:315.7,y:148.3},0).wait(1).to({scaleX:2.59,scaleY:2.59,rotation:10.4,x:317.2,y:146.2},0).wait(1).to({scaleX:2.66,scaleY:2.66,rotation:10.9,x:318.5,y:144},0).wait(1).to({scaleX:2.74,scaleY:2.74,rotation:11.4,x:319.9,y:141.9},0).wait(1).to({scaleX:2.81,scaleY:2.81,rotation:11.9,x:321.3,y:139.9},0).wait(1).to({scaleX:2.89,scaleY:2.89,rotation:12.4,x:322.7,y:137.8},0).wait(1).to({scaleX:2.96,scaleY:2.96,rotation:12.9,x:324.1,y:136},0).wait(1).to({scaleX:3.03,scaleY:3.03,rotation:13.3,x:325.5,y:134},0).wait(1).to({scaleX:3.1,scaleY:3.1,rotation:13.8,x:326.8,y:132.2},0).wait(1).to({scaleX:3.17,scaleY:3.17,rotation:14.3,x:328.2,y:130.4},0).wait(1).to({scaleX:3.24,scaleY:3.24,rotation:14.7,x:329.5,y:128.6},0).wait(1).to({scaleX:3.3,scaleY:3.3,rotation:15.1,x:330.8,y:126.9},0).wait(1).to({scaleX:3.37,scaleY:3.37,rotation:15.6,x:332.1,y:125.2},0).wait(1).to({scaleX:3.44,scaleY:3.44,rotation:16,x:333.4,y:123.6},0).wait(1).to({scaleX:3.5,scaleY:3.5,rotation:16.4,x:334.8,y:122},0).wait(1).to({scaleX:3.56,scaleY:3.56,rotation:16.8,x:336,y:120.4},0).wait(1).to({scaleX:3.63,scaleY:3.63,rotation:17.3,x:337.3,y:118.9},0).wait(1).to({scaleX:3.69,scaleY:3.69,rotation:17.7,x:338.6,y:117.4},0).wait(1).to({scaleX:3.75,scaleY:3.75,rotation:18.1,x:339.8,y:116},0).wait(1).to({scaleX:3.81,scaleY:3.81,rotation:18.4,x:341.1,y:114.6},0).wait(1).to({scaleX:3.87,scaleY:3.87,rotation:18.8,x:342.3,y:113.2},0).wait(1).to({scaleX:3.92,scaleY:3.92,rotation:19.2,x:343.5,y:111.9},0).wait(1).to({scaleX:3.98,scaleY:3.98,rotation:19.6,x:344.7,y:110.6},0).wait(1).to({scaleX:4.04,scaleY:4.04,rotation:19.9,x:345.9,y:109.3},0).wait(1).to({scaleX:4.09,scaleY:4.09,rotation:20.3,x:347.1,y:108.1},0).wait(1).to({scaleX:4.14,scaleY:4.14,rotation:20.7,x:348.2,y:106.9},0).wait(1).to({scaleX:4.2,scaleY:4.2,rotation:21,x:349.4,y:105.8},0).wait(1).to({scaleX:4.25,scaleY:4.25,rotation:21.4,x:350.5,y:104.6},0).wait(1).to({scaleX:4.3,scaleY:4.3,rotation:21.7,x:351.6,y:103.5},0).wait(1).to({scaleX:4.35,scaleY:4.35,rotation:22,x:352.7,y:102.4},0).wait(1).to({scaleX:4.4,scaleY:4.4,rotation:22.4,x:353.8,y:101.4},0).wait(1).to({scaleX:4.45,scaleY:4.45,rotation:22.7,x:354.8,y:100.3},0).wait(1).to({scaleX:4.5,scaleY:4.5,rotation:23,x:355.9,y:99.3},0).wait(1).to({scaleX:4.55,scaleY:4.55,rotation:23.3,x:356.9,y:98.4},0).wait(1).to({scaleX:4.59,scaleY:4.59,rotation:23.6,x:358,y:97.5},0).wait(1).to({scaleX:4.64,scaleY:4.64,rotation:23.9,x:359,y:96.5},0).wait(1).to({scaleX:4.68,scaleY:4.68,rotation:24.2,x:360,y:95.7},0).wait(1).to({scaleX:4.73,scaleY:4.73,rotation:24.5,x:361,y:94.7},0).wait(1).to({scaleX:4.77,scaleY:4.77,rotation:24.8,x:362,y:93.9},0).wait(1).to({scaleX:4.82,scaleY:4.82,rotation:25.1,x:363,y:93.1},0).wait(1).to({regX:0,regY:0,scaleX:4.86,scaleY:4.86,rotation:25.3,x:283.2,y:139},0).wait(1).to({regX:10.9,regY:-15.8,scaleX:4.88,scaleY:4.88,rotation:25.5,x:364.5,y:91.8,alpha:0.96},0).wait(1).to({scaleX:4.91,scaleY:4.91,rotation:25.7,x:365.1,y:91.3,alpha:0.92},0).wait(1).to({scaleX:4.94,scaleY:4.94,rotation:25.9,x:365.7,y:90.9,alpha:0.881},0).wait(1).to({scaleX:4.96,scaleY:4.96,rotation:26.1,x:366.3,y:90.3,alpha:0.843},0).wait(1).to({scaleX:4.99,scaleY:4.99,rotation:26.2,x:366.9,y:89.9,alpha:0.804},0).wait(1).to({scaleX:5.01,scaleY:5.01,rotation:26.4,x:367.5,y:89.5,alpha:0.767},0).wait(1).to({scaleX:5.04,scaleY:5.04,rotation:26.6,x:368.1,y:89,alpha:0.729},0).wait(1).to({scaleX:5.07,scaleY:5.07,rotation:26.7,x:368.6,y:88.6,alpha:0.692},0).wait(1).to({scaleX:5.09,scaleY:5.09,rotation:26.9,x:369.2,y:88.1,alpha:0.656},0).wait(1).to({scaleX:5.11,scaleY:5.11,rotation:27.1,x:369.8,y:87.7,alpha:0.619},0).wait(1).to({scaleX:5.14,scaleY:5.14,rotation:27.2,x:370.3,y:87.3,alpha:0.584},0).wait(1).to({scaleX:5.16,scaleY:5.16,rotation:27.4,x:370.9,y:86.9,alpha:0.548},0).wait(1).to({scaleX:5.19,scaleY:5.19,rotation:27.6,x:371.4,y:86.5,alpha:0.513},0).wait(1).to({scaleX:5.21,scaleY:5.21,rotation:27.7,x:371.9,y:86.1,alpha:0.478},0).wait(1).to({scaleX:5.23,scaleY:5.23,rotation:27.9,x:372.5,y:85.7,alpha:0.444},0).wait(1).to({scaleX:5.26,scaleY:5.26,rotation:28,x:373,y:85.3,alpha:0.41},0).wait(1).to({scaleX:5.28,scaleY:5.28,rotation:28.2,x:373.5,y:85,alpha:0.377},0).wait(1).to({scaleX:5.3,scaleY:5.3,rotation:28.3,x:374,y:84.6,alpha:0.343},0).wait(1).to({scaleX:5.32,scaleY:5.32,rotation:28.5,x:374.5,y:84.2,alpha:0.311},0).wait(1).to({scaleX:5.34,scaleY:5.34,rotation:28.6,x:375.1,y:83.8,alpha:0.278},0).wait(1).to({scaleX:5.37,scaleY:5.37,rotation:28.8,x:375.6,y:83.5,alpha:0.246},0).wait(1).to({scaleX:5.39,scaleY:5.39,rotation:28.9,x:376.1,y:83.1,alpha:0.214},0).wait(1).to({scaleX:5.41,scaleY:5.41,rotation:29.1,x:376.6,y:82.8,alpha:0.182},0).wait(1).to({scaleX:5.43,scaleY:5.43,rotation:29.2,x:377.1,y:82.5,alpha:0.151},0).wait(1).to({scaleX:5.45,scaleY:5.45,rotation:29.4,x:377.5,y:82.2,alpha:0.12},0).wait(1).to({scaleX:5.47,scaleY:5.47,rotation:29.5,x:378,y:81.8,alpha:0.09},0).wait(1).to({scaleX:5.49,scaleY:5.49,rotation:29.6,x:378.5,y:81.5,alpha:0.06},0).wait(1).to({scaleX:5.51,scaleY:5.51,rotation:29.8,x:379,y:81.2,alpha:0.03},0).wait(1).to({regX:0,regY:0,scaleX:5.53,scaleY:5.53,rotation:29.9,x:283.7,y:126.4,alpha:0,visible:false},0).wait(1));

	// Tour
	this.shape = new cjs.Shape();
	this.shape.graphics.f("#5C6268").s().p("AAAACIAAgEIABAAIgBAFg");
	this.shape.setTransform(280.3,223.3);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#5C6268").s().p("AgGACIAGgGIAHAGIgHADg");
	this.shape_1.setTransform(280,222.6);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#5C6268").s().p("AgCACIgCgBIgCgBIADgDIACACIADAAIADgCIACADIgDACIgCACg");
	this.shape_2.setTransform(279.9,225.6);

	this.shape_3 = new cjs.Shape();
	this.shape_3.graphics.f("#5C6268").s().p("AgGABIAAgEIADgBIABACIADACIAFgBIABACIgDACIgEACg");
	this.shape_3.setTransform(279.4,228.4);

	this.shape_4 = new cjs.Shape();
	this.shape_4.graphics.f("#B2C0CE").s().p("AgSAgIAEgJIAOgKIAAgDIACgCIAFgTIgDgDIAEgDIAAgSIAEgBIAFABIAAASIACADIgCAFIgEAVIABAEIgDACIgHAMIgNAHg");
	this.shape_4.setTransform(278.7,226.9);

	this.shape_5 = new cjs.Shape();
	this.shape_5.graphics.f("#664132").s().p("AABAYIgHgxIACABIAEAEIAHAsIgFACg");
	this.shape_5.setTransform(293,248.2);

	this.shape_6 = new cjs.Shape();
	this.shape_6.graphics.f("#664132").s().p("AgGAXIAEgjIACAAIAHgOIgGAzIgFACg");
	this.shape_6.setTransform(283.9,251.2);

	this.shape_7 = new cjs.Shape();
	this.shape_7.graphics.f("#664132").s().p("AAEgnIAFACIgLBHIgGAGg");
	this.shape_7.setTransform(272.9,251.1);

	this.shape_8 = new cjs.Shape();
	this.shape_8.graphics.f("#664132").s().p("AgmANIBOgdIACACIhTAgg");
	this.shape_8.setTransform(291.9,259.2);

	this.shape_9 = new cjs.Shape();
	this.shape_9.graphics.f("#664132").s().p("AAAAXIAAgvIAAAAIABACIAAAvg");
	this.shape_9.setTransform(296.1,260.1);

	this.shape_10 = new cjs.Shape();
	this.shape_10.graphics.f("#664132").s().p("Ag4gOIgCgBIABgCIACAAIBxAfIACAEg");
	this.shape_10.setTransform(275,260.4);

	this.shape_11 = new cjs.Shape();
	this.shape_11.graphics.f("#664132").s().p("AgBAUIAAgnIABABIACgEIAAAqIgCACg");
	this.shape_11.setTransform(287.2,263.7);

	this.shape_12 = new cjs.Shape();
	this.shape_12.graphics.f("#664132").s().p("AAAAVIAAgoIAAgBIAAABIABAog");
	this.shape_12.setTransform(285.7,263.3);

	this.shape_13 = new cjs.Shape();
	this.shape_13.graphics.f("#664132").s().p("AgBAqIAAhTIADAJIAABKg");
	this.shape_13.setTransform(279.5,263.5);

	this.shape_14 = new cjs.Shape();
	this.shape_14.graphics.f("#664132").s().p("AiBAcIgbhBIABgBIACAAIAaA/IBOAXIA8gWIAOAGIBYghIAAgIIAZgKIAQggIADAAIgQAhIgaAMIAAAGIhZAjIgPgEIg8AVg");
	this.shape_14.setTransform(284.6,262.7);

	this.shape_15 = new cjs.Shape();
	this.shape_15.graphics.f("#664132").s().p("AgYAJIAxgYIgBAHIgwAYg");
	this.shape_15.setTransform(286.7,274.1);

	this.shape_16 = new cjs.Shape();
	this.shape_16.graphics.f("#664132").s().p("AgggGIgCgHIBFAUIAAAHg");
	this.shape_16.setTransform(279.6,274.4);

	this.shape_17 = new cjs.Shape();
	this.shape_17.graphics.f("#664132").s().p("AgHAqIAIhQIAHgEIgIBTIgEABg");
	this.shape_17.setTransform(289.9,269);

	this.shape_18 = new cjs.Shape();
	this.shape_18.graphics.f("#664132").s().p("AACAkIgMhIIAIACIANBGIgEABg");
	this.shape_18.setTransform(275.2,270);

	this.shape_19 = new cjs.Shape();
	this.shape_19.graphics.f("#664132").s().p("AgFAvIgBhbIAMgFIABBfIgGADg");
	this.shape_19.setTransform(283.7,271.3);

	this.shape_20 = new cjs.Shape();
	this.shape_20.graphics.f("#405B5A").s().p("Ag0BcQgXgHAAgMQAEgQAfg5QAhg8AJgnQALArAcA4QAgBAADAJQAAAMgWAHQgWAIggAAQgfAAgVgIg");
	this.shape_20.setTransform(274.5,228.5);

	this.shape_21 = new cjs.Shape();
	this.shape_21.graphics.f("#405B5A").s().p("AhrAfIgDgFIANgLIAuhMIBwAcIAnA3IALAKIgDAEIgMgEIAAgFIglg2Ig0BYg");
	this.shape_21.setTransform(283.2,243.7);

	this.shape_22 = new cjs.Shape();
	this.shape_22.graphics.f("#405B5A").s().p("AgiBSIg5hjIhCA+IgEACIgDgEIBJhGIA2gPIAbAkIAvgOIAKgMIAJAGIAfgNIgHglIAUgFIAzBXIAOAQIAAAGIgyAIIgjgsIg4BRIgPgGIgmAPg");
	this.shape_22.setTransform(284.7,254.4);

	this.shape_23 = new cjs.Shape();
	this.shape_23.graphics.f("#664132").s().p("AgMAPIAPgwIAKgDIgZBJg");
	this.shape_23.setTransform(293.9,266.9);

	this.shape_24 = new cjs.Shape();
	this.shape_24.graphics.f("#664132").s().p("AANAaIgegxIAKgEIAYAtIAAAKg");
	this.shape_24.setTransform(289.8,268.4);

	this.shape_25 = new cjs.Shape();
	this.shape_25.graphics.f("#664132").s().p("AgGBQIAAidIANgGIAACjIgHAEg");
	this.shape_25.setTransform(291.8,272.6);

	this.shape_26 = new cjs.Shape();
	this.shape_26.graphics.f("#664132").s().p("AgGASIAGglIAHAAIgHAmIgEABg");
	this.shape_26.setTransform(276,240.4);

	this.shape_27 = new cjs.Shape();
	this.shape_27.graphics.f("#664132").s().p("AgPAYIAYgvIAHAAIgYAtIgEACg");
	this.shape_27.setTransform(280.1,239.2);

	this.shape_28 = new cjs.Shape();
	this.shape_28.graphics.f("#664132").s().p("AAIAWIgVgtIAGAAIAWAuIgDABg");
	this.shape_28.setTransform(269.1,239.5);

	this.shape_29 = new cjs.Shape();
	this.shape_29.graphics.f("#664132").s().p("AAAAUIgGgoIAGAAIAHAmIgBADg");
	this.shape_29.setTransform(272.3,240.4);

	this.shape_30 = new cjs.Shape();
	this.shape_30.graphics.f("#664132").s().p("AgDAKQgagEAAgGIAAgJIADgCIABADQACAGAUACQAOACATgCIgBAKQgHACgJAAIgQgCg");
	this.shape_30.setTransform(272.6,242.1);

	this.shape_31 = new cjs.Shape();
	this.shape_31.graphics.f("#664132").s().p("AgUAAIAAgGIAJABIADgBIATADIAAACIAKABIAAAHg");
	this.shape_31.setTransform(279.2,285.4);

	this.shape_32 = new cjs.Shape();
	this.shape_32.graphics.f("#664132").s().p("AglAAIAAgOIBLAOIAAAPg");
	this.shape_32.setTransform(278.9,292);

	this.shape_33 = new cjs.Shape();
	this.shape_33.graphics.f("#664132").s().p("AgEgJIACgJIAEACIADAQIgEAAIgDATg");
	this.shape_33.setTransform(273.7,274.8);

	this.shape_34 = new cjs.Shape();
	this.shape_34.graphics.f("#664132").s().p("AgcAEIAygYIAHABIAAAKIg3Aeg");
	this.shape_34.setTransform(290.8,277.4);

	this.shape_35 = new cjs.Shape();
	this.shape_35.graphics.f("#664132").s().p("Ag3AAIAAgMIAIgDIBiAPIAFAGIAAAKg");
	this.shape_35.setTransform(280.4,278.4);

	this.shape_36 = new cjs.Shape();
	this.shape_36.graphics.f("#664132").s().p("AggBEIASgYQAEgGAhhuIAFgIIAFADIgEAJIguCUg");
	this.shape_36.setTransform(271.3,279.2);

	this.shape_37 = new cjs.Shape();
	this.shape_37.graphics.f("#664132").s().p("AAFBmIhGjIIADgHIAIAAQBACKAHAMQAIAMApAjIgDAOg");
	this.shape_37.setTransform(299.2,283.5);

	this.shape_38 = new cjs.Shape();
	this.shape_38.graphics.f("#664132").s().p("Ag3BtQAMgIAkg2QAHgQAjiPIAJgFIAMAHIgsDTIg3ARg");
	this.shape_38.setTransform(270.3,286.2);

	this.shape_39 = new cjs.Shape();
	this.shape_39.graphics.f("#664132").s().p("AgRBkIgLjtIAQgDIAIAJIAJCgQABAOAKAqQAKArAEAHIgZAGg");
	this.shape_39.setTransform(288.5,290.3);

	this.shape_40 = new cjs.Shape();
	this.shape_40.graphics.f("#77543D").s().p("AgWAUIAAgxIAsAKIgBAwg");
	this.shape_40.setTransform(279,295);

	this.shape_41 = new cjs.Shape();
	this.shape_41.graphics.f("#68615F").s().p("AgNgDIAAgNIANgHIAMAHIACAfIgNAJg");
	this.shape_41.setTransform(292,282.3);

	this.shape_42 = new cjs.Shape();
	this.shape_42.graphics.f("#273435").s().p("AgGgGIANgGIAFAQIgXAJg");
	this.shape_42.setTransform(289.1,247.4);

	this.shape_43 = new cjs.Shape();
	this.shape_43.graphics.f("#273435").s().p("AgPgCIAfgNIAAATIgfAMg");
	this.shape_43.setTransform(291.8,262.2);

	this.shape_44 = new cjs.Shape();
	this.shape_44.graphics.f("#273435").s().p("AgJAOIAAgWIACgEQAEgFADABQAEAAAEAFQACADAAADIAAAWg");
	this.shape_44.setTransform(279.3,283.2);

	this.shape_45 = new cjs.Shape();
	this.shape_45.graphics.f("#E0DCCD").s().p("AhVAUIgIgeIA2gZIBoAIIAdAfIhDAgg");
	this.shape_45.setTransform(283.5,272.9);

	this.shape_46 = new cjs.Shape();
	this.shape_46.graphics.f("#E0DCCD").s().p("AgqCAIAAgHIgtgJIgHAEIg4gMIgJheIAuiVIAJAgIBwAQIBDggIBVDCIiVBFg");
	this.shape_46.setTransform(285.5,285.7);

	this.shape_47 = new cjs.Shape();
	this.shape_47.graphics.f("#E0DCCD").s().p("AhAAvIgThvICngFIgPBuIg5Adg");
	this.shape_47.setTransform(282.4,268.4);

	this.shape_48 = new cjs.Shape();
	this.shape_48.graphics.f("#E0DCCD").s().p("AgrBeQgTgFgEgLQgBgKAMgiQALgjABghIgXg2ICGgHIgJAZIgagKIgbAwIgdCAQgKAAgKgCg");
	this.shape_48.setTransform(274.6,245.5);

	this.shape_49 = new cjs.Shape();
	this.shape_49.graphics.f("#E0DCCD").s().p("AgTAUIAcgzIALA/g");
	this.shape_49.setTransform(277.2,242.8);

	this.shape_50 = new cjs.Shape();
	this.shape_50.graphics.f("#E0DCCD").s().p("AhggKIBjAZIA5hYIAkA5IAPA2IhlAZIghgqIg0ARIgjAkg");
	this.shape_50.setTransform(283.1,248.2);

	this.shape_51 = new cjs.Shape();
	this.shape_51.graphics.f("#E0DCCD").s().p("AiCA3IgbhCIBDhDIA5BVIA6AAIA4hKIAkAsIApgFIgSAjIgZAMIAAAIIhaAjIgPgFIg8AWg");
	this.shape_51.setTransform(284.7,260);

	this.shape_52 = new cjs.Shape();
	this.shape_52.graphics.f("#68615F").s().p("Ag7AvQgbgIgCgLIgCg1QAAgMAbgIQAbgJAkAAQAlAAAbAJQAbAIAAAMQAAARgaAcQgeAjgiAAQgiAAgagIg");
	this.shape_52.setTransform(274.6,256.2);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.shape_52},{t:this.shape_51},{t:this.shape_50},{t:this.shape_49},{t:this.shape_48},{t:this.shape_47},{t:this.shape_46},{t:this.shape_45},{t:this.shape_44},{t:this.shape_43},{t:this.shape_42},{t:this.shape_41},{t:this.shape_40},{t:this.shape_39},{t:this.shape_38},{t:this.shape_37},{t:this.shape_36},{t:this.shape_35},{t:this.shape_34},{t:this.shape_33},{t:this.shape_32},{t:this.shape_31},{t:this.shape_30},{t:this.shape_29},{t:this.shape_28},{t:this.shape_27},{t:this.shape_26},{t:this.shape_25},{t:this.shape_24},{t:this.shape_23},{t:this.shape_22},{t:this.shape_21},{t:this.shape_20},{t:this.shape_19},{t:this.shape_18},{t:this.shape_17},{t:this.shape_16},{t:this.shape_15},{t:this.shape_14},{t:this.shape_13},{t:this.shape_12},{t:this.shape_11},{t:this.shape_10},{t:this.shape_9},{t:this.shape_8},{t:this.shape_7},{t:this.shape_6},{t:this.shape_5},{t:this.shape_4},{t:this.shape_3},{t:this.shape_2},{t:this.shape_1},{t:this.shape}]}).to({state:[{t:this.shape_52},{t:this.shape_51},{t:this.shape_50},{t:this.shape_49},{t:this.shape_48},{t:this.shape_47},{t:this.shape_46},{t:this.shape_45},{t:this.shape_44},{t:this.shape_43},{t:this.shape_42},{t:this.shape_41},{t:this.shape_40},{t:this.shape_39},{t:this.shape_38},{t:this.shape_37},{t:this.shape_36},{t:this.shape_35},{t:this.shape_34},{t:this.shape_33},{t:this.shape_32},{t:this.shape_31},{t:this.shape_30},{t:this.shape_29},{t:this.shape_28},{t:this.shape_27},{t:this.shape_26},{t:this.shape_25},{t:this.shape_24},{t:this.shape_23},{t:this.shape_22},{t:this.shape_21},{t:this.shape_20},{t:this.shape_19},{t:this.shape_18},{t:this.shape_17},{t:this.shape_16},{t:this.shape_15},{t:this.shape_14},{t:this.shape_13},{t:this.shape_12},{t:this.shape_11},{t:this.shape_10},{t:this.shape_9},{t:this.shape_8},{t:this.shape_7},{t:this.shape_6},{t:this.shape_5},{t:this.shape_4},{t:this.shape_3},{t:this.shape_2},{t:this.shape_1},{t:this.shape}]},85).wait(1));

	// Sol
	this.shape_53 = new cjs.Shape();
	this.shape_53.graphics.f("#93C92B").s().p("AisBaQhIglAAg1QAAgzBIgmQBIgmBkAAQBlAABIAmQBHAmABAzQgBA1hHAlQhIAmhlAAQhkAAhIgmg");
	this.shape_53.setTransform(284.5,292.8);

	this.timeline.addTween(cjs.Tween.get(this.shape_53).wait(86));

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(535,410.7,49,94.9);

})(lib = lib||{}, images = images||{}, createjs = createjs||{}, ss = ss||{});
var lib, images, createjs, ss;