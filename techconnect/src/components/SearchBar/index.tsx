import * as React from "react";
import { Check } from "lucide-react";
import { FaSearch } from "react-icons/fa";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
} from "@/components/ui/command";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { useUserData } from "@/hooks/userHooks/useUserData";
import { useRouter } from "next/navigation";
import Image from "next/image";

export default function SearchBar() {
  const [open, setOpen] = React.useState(false);
  const [value, setValue] = React.useState("");

  const router = useRouter();

  const { data: users } = useUserData();

  const navigateToProfile = (userId: string) => {
    router.push(`/profile/${userId}`);
  };

  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger asChild>
        <Button
          variant="outline"
          role="combobox"
          aria-expanded={open}
          className="w-1/3 md:w-[304px] justify-between md:flex overflow-x-auto md:overflow-hidden"
        >
          {value && users
            ? users.find((user) => user.nome === value)?.nome
            : "Buscar usuário..."}
          <FaSearch className="ml-2 h-4 w-4 shrink-0 opacity-50" />
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-max md:w-[304px] p-0">
        <Command>
          <CommandInput
            placeholder="Buscar usuário..."
            onValueChange={(val) => setValue(val)} // Atualiza o value com o input
          />
          <CommandList>
            <CommandEmpty>Nenhum usuário encontrado.</CommandEmpty>
            <CommandGroup>
              {users &&
                value &&
                users
                  .filter((user) =>
                    user.nome
                      .toLowerCase()
                      .normalize("NFD")
                      .replace(/[\u0300-\u036f]/g, "")
                      .includes(
                        value
                          .toLowerCase()
                          .normalize("NFD")
                          .replace(/[\u0300-\u036f]/g, "")
                      )
                  )
                  .map((user) => (
                    <CommandItem
                      className="cursor-pointer"
                      key={user.idUsuario}
                      value={user.nome}
                      onSelect={(currentValue) => {
                        navigateToProfile(user.idUsuario);
                        setValue(currentValue === value ? "" : currentValue);
                        setOpen(false);
                      }}
                    >
                      <Check
                        className={cn(
                          "mr-2 h-4 w-4",
                          value === user.nome ? "opacity-100" : "opacity-0"
                        )}
                      />
                      <Image
                        width={100}
                        height={100}
                        className="w-5 rounded-full mr-2"
                        src={user.urlMidia}
                        alt="Imagem do usuário pesquisado."
                      />
                      {/* <p>{user.urlMidia}</p> */}
                      {user.nome}
                    </CommandItem>
                  ))}
            </CommandGroup>
          </CommandList>
        </Command>
      </PopoverContent>
    </Popover>
  );
}
