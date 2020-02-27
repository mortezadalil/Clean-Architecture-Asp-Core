using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Api.Presenter;
using Cms.Core.Commands;
using Cms.Core.Dtos.UseCase;
using Cms.Core.Interfaces.Repository;
using Cms.Core.Queries;
using Cms.Core.UseCase;
using Cms.Core.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cms.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PostController : BaseController
  {
    public IMediator _mediatR { get; }
    public IEditPostUseCase _editPostUseCase { get; }
    public Presenter.PostApiPresenter<EditPostResponse> _postApiPresenter { get; }

    public PostController(IMediator mediatR,
      IEditPostUseCase editPostUseCase,
      PostApiPresenter<EditPostResponse> postApiPresenter)
    {
      _mediatR = mediatR;
      _editPostUseCase = editPostUseCase;
      _postApiPresenter = postApiPresenter;
    }



    // post/grtlatestposts
    [HttpGet("GetLatestPosts")]
    public async Task<IActionResult> GetLatestPosts()
    {
      var query = new GetLatestPostsQuery();
      var result = await _mediatR.Send(query);
      return CustomOk(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PostAddVm post)
    {
      var command = new PostAddCommand
      {
        Content = post.Content,
        Title = post.Title
      };

      var result = await _mediatR.Send(command);
      return CustomOk(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(PostEditVm model)
    {
      var post = new EditPostRequest
      {
        Id=model.Id,
        Content = model.Content,
        Title = model.Title
      };

      await _editPostUseCase.HandleAsync(post, _postApiPresenter);
      return _postApiPresenter.ContentResult;
    }

  }
}
