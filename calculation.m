rgb=imread('polluted sky.jpeg');
[width, height, channel]=size(rgb);
rpcount=0;
for i=1:width
for j=1:height
if image(i,j,1)==255&image(i,j,2)==0&image(i,j,3)==0
rpcount=rpcount+1;
end
end
end
disp(rpcount);