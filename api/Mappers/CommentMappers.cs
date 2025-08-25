using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockSymbol = comment.Stock?.Symbol ?? string.Empty,
                Username = comment.AppUser?.UserName ?? string.Empty
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                CreatedOn = DateTime.Now,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto updateDto, int id)
        {
            return new Comment
            {
                Id = id,
                Title = updateDto.Title,
                Content = updateDto.Content
            };
        }
    }
}   