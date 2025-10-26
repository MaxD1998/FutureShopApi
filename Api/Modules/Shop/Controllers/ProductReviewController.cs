﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Shared.Dtos;
using Shop.Core.Dtos.ProductReview;
using Shop.Core.Services;

namespace Api.Modules.Shop.Controllers;

public class ProductReviewController(IProductReviewService productReviewService) : ShopModuleBaseController
{
    private readonly IProductReviewService _productReviewService = productReviewService;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ProductReviewRequestFormDto dto, CancellationToken cancellationToken)
        => await ApiResponseAsync(_productReviewService.CreateAsync, dto, cancellationToken);

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        => await ApiResponseAsync(_productReviewService.DeleteByIdAsync, id, cancellationToken);

    [HttpGet("Page/{productId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPageByProductIdAsync([FromRoute] Guid productId, [FromQuery] PaginationDto pagination, CancellationToken cancellationToken)
        => await ApiResponseAsync(_productReviewService.GetPageByProductIdAsync, productId, pagination, cancellationToken);

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductReviewRequestFormDto dto, CancellationToken cancellationToken)
        => await ApiResponseAsync(_productReviewService.UpdateAsync, id, dto, cancellationToken);
}