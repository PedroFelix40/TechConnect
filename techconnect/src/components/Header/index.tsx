import SearchBar from "../SearchBar";
import ImageComponent from "../ImageComponent";
import ContextMenuHeader from "./ContextMenuHeader";
import Link from "next/link";
import { motion } from "framer-motion";
import { usePathname } from "next/navigation";

const Header = () => {
  const pathname = usePathname();

  const goToTop = () => {
    if (pathname === "/") {
      window.scrollTo({
        top: 0,
        behavior: "smooth",
      });
    }
  };
  return (
    <motion.header
      initial={{ top: -100 }}
      animate={{ top: 0 }}
      transition={{ duration: 0.3 }}
      className="flex items-center fixed z-[50] bg-complementary-white top-0 justify-between w-full h-20 p-2 border-b-2 rounded-b-[10px] md:px-20"
    >
      <Link
        onClick={goToTop}
        className="transition-all ease-in-out duration-500 hover:scale-125"
        href={{
          pathname: "/",
        }}
      >
        <ImageComponent src={"/images/dark-logo.png"} alt="Logo da empresa." />
      </Link>
      <SearchBar />

      <div className="flex items-center justify-end w-max gap-4">
        <ContextMenuHeader />
      </div>
    </motion.header>
  );
};

export default Header;
