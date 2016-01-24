#!/bin/bash

echo "Beginning image to tile conversion";
start=`date +%s`;
find . -type f -name '*.png' -exec sh -c '
for i do
	SIZE=4096
	NAME=${i//.png};
	echo "Starting $NAME...";
	JUSTNAME=${NAME##*/}
	mkdir "$NAME";
	istart=`date +%s`;
	convert +repage -crop "$SIZE"x"$SIZE" "$i" "$NAME"/"$JUSTNAME"_%x.png;
	iend=`date +%s`;
	itime=$((iend-istart));
	echo "Finished $NAME in $itime seconds!";
done
' sh {} +
end=`date +%s`;
time=$((end-start));
echo "Finished image to tile conversion in $time seconds!";