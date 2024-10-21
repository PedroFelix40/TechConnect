import {
  ContextMenu,
  ContextMenuContent,
  ContextMenuItem,
  ContextMenuTrigger,
} from "@radix-ui/react-context-menu";
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/components/ui/tooltip";
import React from "react";
import { FaEllipsisV } from "react-icons/fa";
import { useDeletePost } from "@/hooks/postHooks/useDeletePost";

const ContextMenuCardPost = ({
  postId,
  userId,
}: {
  postId: string;
  userId: string;
}) => {
  const { mutate: deletePostMutate } = useDeletePost(userId);
  const handleDeletePost = () => {
    deletePostMutate(postId);
  };
  const handleUpdatePost = () => {};

  return (
    <ContextMenu>
      <ContextMenuTrigger>
        <TooltipProvider>
          <Tooltip>
            <TooltipTrigger>
              <FaEllipsisV className="transition-all ease-in-out duration-500 hover:scale-150 cursor-pointer" />
            </TooltipTrigger>
            <TooltipContent className="mb-4">
              <span>Clique com o direito</span>
            </TooltipContent>
          </Tooltip>
        </TooltipProvider>
      </ContextMenuTrigger>
      <ContextMenuContent className="bg-complementary-white !mr-20 p-5 border !rounded-md border-complementary-black">
        <ContextMenuItem
          onClick={handleDeletePost}
          className="text-[#E71A1A] flex gap-2 hover:cursor-pointer"
        >
          Excluir Post
        </ContextMenuItem>
        {/* <ContextMenuItem
          onClick={handleUpdatePost}
          className="flex gap-2 hover:cursor-pointer"
        >
          Atualizar Post
        </ContextMenuItem> */}
      </ContextMenuContent>
    </ContextMenu>
  );
};

export default ContextMenuCardPost;
