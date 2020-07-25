#!/usr/bin/env python

import sys
import os

name = sys.argv[1]
os.system("D:\\Steam\\steamapps\\common\\Aseprite\\Aseprite.exe -b --trim --sheet-pack "
		  + "--inner-padding 1 "
	      + "--split-layers D:\\Immanence\\Art\Rooms\\" + name + ".ase "
	      + "--sheet Images\\" + name + ".png "
	      + "--data Data\\" + name + ".json")