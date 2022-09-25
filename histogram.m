clc
clear all
clc

#Read an RGB image
image=imread('unpolluted sky.PNG');

#Extract color channels
  #Red channel
  red=image(:,:,1);
  #Green channel
  green=image(:,:,2);
  #Blue channel
  blue=image(:,:,3);
  
#Just black channel
justBlack=zeros(size(image, 1), size(image, 2), 'uint8');

#Create RGB channels
AllRed=cat(3, red, justBlack, justBlack);
AllGreen = cat(3, justBlack, green, justBlack);
AllBlue = cat(3, justBlack, justBlack, blue);

%Displaying RGB images
subplot(3, 3, 2);
imshow(image);
title('Original RGB Image')
subplot(3, 3, 4);
imshow(AllRed);
title('Red Channel')
subplot(3, 3, 5);
imshow(AllGreen)
title('Green Channel')
subplot(3, 3, 6);
imshow(AllBlue);
title('Blue Channel')

#Displaying RGB Histogram
red=imhist(image(:,:,1));
green=imhist(image(:,:,2));
blue=imhist(image(:,:,3));
figure
plot(red,'r') 
hold on, 
plot(green,'g') 
hold on
plot(blue,'b')
legend(' Red channel','Green channel','Blue channel');
hold off

