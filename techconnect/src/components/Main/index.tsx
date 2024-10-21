import { motion } from "framer-motion";
import { ReactNode } from "react";

const Main = ({
  children,
  isAuthPage,
}: {
  children: ReactNode;
  isAuthPage?: boolean;
}) => {
  const mainAuthStyle = `w-screen h-screen overflow-hidden`;
  const defaultStyle = `w-full h-full mt-20 flex justify-center overflow-hidden`;

  const animation = {
    initial: { opacity: 0 },
    animate: { opacity: 1 },
    exit: { opacity: 0 },
    transition: { duration: 0.5 },
  };

  return (
    <motion.main
      {...animation}
      className={isAuthPage ? mainAuthStyle : defaultStyle}
    >
      {children}
    </motion.main>
  );
};

export default Main;
