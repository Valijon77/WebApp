﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Validation;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

//[PhraseAndPrice(Phrase ="Small",Price ="100")]
public class Product
{
    public long ProductId { get; set; }

    [Required(ErrorMessage = "Please enter a valueueu")]
    public string Name { get; set; } = string.Empty;


    //[DisplayFormat(DataFormatString = "{0:c2}", ApplyFormatInEditMode = true)]
    [Range(1, 999999, ErrorMessage ="Please enter a positive priceey")]
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    [PrimaryKey(ContextType =typeof(DataContext), DataType =typeof(Category))]
    //[Remote("CategoryKey", "Validation", ErrorMessage ="Enter an existing keyya")]
    public long CategoryId { get; set; }

    public Category? Category { get; set; }

    [PrimaryKey(ContextType =typeof(DataContext), DataType =typeof(Supplier))]
    //[Remote("SupplierKey", "Validation", ErrorMessage ="Enter an existing keyyaaa")]
    public long SupplierId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Supplier? Supplier { get; set; }
}