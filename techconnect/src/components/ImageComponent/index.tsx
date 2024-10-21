import Image, { ImageProps } from "next/image";

type imageProps = ImageProps & {};

const ImageComponent = ({ ...rest }: imageProps) => {
  return <Image width={80} height={80} {...rest} />;
};

export default ImageComponent;
