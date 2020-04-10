//01.11.2017 12:50:03
using System;
using System.Windows;

namespace Zweistein
	{
		public partial class SpreadTraderWindow : Window
		{
private static byte[] baml = 
	{
		0x0C, 0x00, 0x00, 0x00, 0x4D, 0x00, 0x53, 0x00, 0x42, 0x00, 0x41, 0x00, 0x4D, 0x00, 0x4C, 
		0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x60, 0x00, 0x01, 0x00, 
		0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x1C, 0x4D, 0x00, 0x00, 0x49, 0x53, 0x70, 0x72, 0x65, 0x61, 
		0x64, 0x54, 0x72, 0x61, 0x64, 0x65, 0x72, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x2C, 0x20, 
		0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x31, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 
		0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 0x72, 
		0x61, 0x6C, 0x2C, 0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F, 
		0x6B, 0x65, 0x6E, 0x3D, 0x6E, 0x75, 0x6C, 0x6C, 0x1B, 0x25, 0x17, 0x63, 0x6C, 0x72, 0x2D, 
		0x6E, 0x61, 0x6D, 0x65, 0x73, 0x70, 0x61, 0x63, 0x65, 0x3A, 0x5A, 0x77, 0x65, 0x69, 0x73, 
		0x74, 0x65, 0x69, 0x6E, 0x09, 0x5A, 0x77, 0x65, 0x69, 0x73, 0x74, 0x65, 0x69, 0x6E, 0x00, 
		0x00, 0x35, 0x06, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x1B, 0x25, 0x17, 0x63, 0x6C, 
		0x72, 0x2D, 0x6E, 0x61, 0x6D, 0x65, 0x73, 0x70, 0x61, 0x63, 0x65, 0x3A, 0x49, 0x4B, 0x72, 
		0x69, 0x76, 0x2E, 0x57, 0x70, 0x66, 0x09, 0x49, 0x4B, 0x72, 0x69, 0x76, 0x2E, 0x57, 0x70, 
		0x66, 0x00, 0x00, 0x35, 0x08, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x1D, 0x22, 0x00, 
		0x00, 0x00, 0x00, 0x1C, 0x5A, 0x77, 0x65, 0x69, 0x73, 0x74, 0x65, 0x69, 0x6E, 0x2E, 0x53, 
		0x70, 0x72, 0x65, 0x61, 0x64, 0x54, 0x72, 0x61, 0x64, 0x65, 0x72, 0x57, 0x69, 0x6E, 0x64, 
		0x6F, 0x77, 0x03, 0x00, 0x00, 0x00, 0x35, 0x01, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 
		0x1C, 0x57, 0x01, 0x00, 0x53, 0x50, 0x72, 0x65, 0x73, 0x65, 0x6E, 0x74, 0x61, 0x74, 0x69, 
		0x6F, 0x6E, 0x43, 0x6F, 0x72, 0x65, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 
		0x3D, 0x34, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 
		0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C, 0x20, 0x50, 0x75, 0x62, 
		0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F, 0x6B, 0x65, 0x6E, 0x3D, 0x33, 0x31, 0x62, 
		0x66, 0x33, 0x38, 0x35, 0x36, 0x61, 0x64, 0x33, 0x36, 0x34, 0x65, 0x33, 0x35, 0x1C, 0x52, 
		0x02, 0x00, 0x4E, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73, 0x42, 0x61, 0x73, 0x65, 0x2C, 
		0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 
		0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 
		0x72, 0x61, 0x6C, 0x2C, 0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 
		0x6F, 0x6B, 0x65, 0x6E, 0x3D, 0x33, 0x31, 0x62, 0x66, 0x33, 0x38, 0x35, 0x36, 0x61, 0x64, 
		0x33, 0x36, 0x34, 0x65, 0x33, 0x35, 0x1C, 0x5C, 0x03, 0x00, 0x58, 0x50, 0x72, 0x65, 0x73, 
		0x65, 0x6E, 0x74, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 
		0x72, 0x6B, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34, 0x2E, 0x30, 
		0x2E, 0x30, 0x2E, 0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E, 
		0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C, 0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 
		0x65, 0x79, 0x54, 0x6F, 0x6B, 0x65, 0x6E, 0x3D, 0x33, 0x31, 0x62, 0x66, 0x33, 0x38, 0x35, 
		0x36, 0x61, 0x64, 0x33, 0x36, 0x34, 0x65, 0x33, 0x35, 0x14, 0x44, 0x00, 0x39, 0x68, 0x74, 
		0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 0x73, 0x2E, 0x6D, 0x69, 
		0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x63, 0x6F, 0x6D, 0x2F, 0x77, 0x69, 0x6E, 
		0x66, 0x78, 0x2F, 0x32, 0x30, 0x30, 0x36, 0x2F, 0x78, 0x61, 0x6D, 0x6C, 0x2F, 0x70, 0x72, 
		0x65, 0x73, 0x65, 0x6E, 0x74, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x03, 0x00, 0x01, 0x00, 0x02, 
		0x00, 0x03, 0x00, 0x35, 0x02, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x1C, 0x52, 0x04, 
		0x00, 0x4E, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x58, 0x61, 0x6D, 0x6C, 0x2C, 0x20, 
		0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 
		0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 0x72, 
		0x61, 0x6C, 0x2C, 0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F, 
		0x6B, 0x65, 0x6E, 0x3D, 0x62, 0x37, 0x37, 0x61, 0x35, 0x63, 0x35, 0x36, 0x31, 0x39, 0x33, 
		0x34, 0x65, 0x30, 0x38, 0x39, 0x14, 0x3A, 0x01, 0x78, 0x2C, 0x68, 0x74, 0x74, 0x70, 0x3A, 
		0x2F, 0x2F, 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 0x73, 0x2E, 0x6D, 0x69, 0x63, 0x72, 0x6F, 
		0x73, 0x6F, 0x66, 0x74, 0x2E, 0x63, 0x6F, 0x6D, 0x2F, 0x77, 0x69, 0x6E, 0x66, 0x78, 0x2F, 
		0x32, 0x30, 0x30, 0x36, 0x2F, 0x78, 0x61, 0x6D, 0x6C, 0x04, 0x00, 0x01, 0x00, 0x02, 0x00, 
		0x04, 0x00, 0x03, 0x00, 0x35, 0x03, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x14, 0x40, 
		0x01, 0x64, 0x32, 0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x73, 0x63, 0x68, 0x65, 0x6D, 
		0x61, 0x73, 0x2E, 0x6D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2E, 0x63, 0x6F, 
		0x6D, 0x2F, 0x65, 0x78, 0x70, 0x72, 0x65, 0x73, 0x73, 0x69, 0x6F, 0x6E, 0x2F, 0x62, 0x6C, 
		0x65, 0x6E, 0x64, 0x2F, 0x32, 0x30, 0x30, 0x38, 0x04, 0x00, 0x01, 0x00, 0x02, 0x00, 0x04, 
		0x00, 0x03, 0x00, 0x35, 0x04, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x14, 0x4A, 0x02, 
		0x6D, 0x63, 0x3B, 0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x73, 0x63, 0x68, 0x65, 0x6D, 
		0x61, 0x73, 0x2E, 0x6F, 0x70, 0x65, 0x6E, 0x78, 0x6D, 0x6C, 0x66, 0x6F, 0x72, 0x6D, 0x61, 
		0x74, 0x73, 0x2E, 0x6F, 0x72, 0x67, 0x2F, 0x6D, 0x61, 0x72, 0x6B, 0x75, 0x70, 0x2D, 0x63, 
		0x6F, 0x6D, 0x70, 0x61, 0x74, 0x69, 0x62, 0x69, 0x6C, 0x69, 0x74, 0x79, 0x2F, 0x32, 0x30, 
		0x30, 0x36, 0x04, 0x00, 0x01, 0x00, 0x02, 0x00, 0x04, 0x00, 0x03, 0x00, 0x35, 0x05, 0x00, 
		0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x14, 0x29, 0x05, 0x6C, 0x6F, 0x63, 0x61, 0x6C, 0x17, 
		0x63, 0x6C, 0x72, 0x2D, 0x6E, 0x61, 0x6D, 0x65, 0x73, 0x70, 0x61, 0x63, 0x65, 0x3A, 0x5A, 
		0x77, 0x65, 0x69, 0x73, 0x74, 0x65, 0x69, 0x6E, 0x04, 0x00, 0x01, 0x00, 0x02, 0x00, 0x04, 
		0x00, 0x03, 0x00, 0x35, 0x06, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x1C, 0x67, 0x05, 
		0x00, 0x63, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 
		0x73, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x72, 0x6F, 0x6C, 0x73, 0x2E, 0x57, 0x70, 0x66, 0x50, 
		0x72, 0x6F, 0x70, 0x65, 0x72, 0x74, 0x79, 0x47, 0x72, 0x69, 0x64, 0x2C, 0x20, 0x56, 0x65, 
		0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x32, 0x30, 0x31, 0x30, 0x2E, 0x31, 0x31, 0x2E, 0x31, 
		0x30, 0x2E, 0x30, 0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 
		0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C, 0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 
		0x79, 0x54, 0x6F, 0x6B, 0x65, 0x6E, 0x3D, 0x6E, 0x75, 0x6C, 0x6C, 0x14, 0x40, 0x02, 0x70, 
		0x67, 0x37, 0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 
		0x73, 0x2E, 0x64, 0x65, 0x6E, 0x69, 0x73, 0x76, 0x75, 0x79, 0x6B, 0x61, 0x2E, 0x77, 0x6F, 
		0x72, 0x64, 0x70, 0x72, 0x65, 0x73, 0x73, 0x2E, 0x63, 0x6F, 0x6D, 0x2F, 0x77, 0x70, 0x66, 
		0x70, 0x72, 0x6F, 0x70, 0x65, 0x72, 0x74, 0x79, 0x67, 0x72, 0x69, 0x64, 0x01, 0x00, 0x05, 
		0x00, 0x35, 0x07, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x14, 0x23, 0x05, 0x69, 0x6B, 
		0x72, 0x69, 0x76, 0x17, 0x63, 0x6C, 0x72, 0x2D, 0x6E, 0x61, 0x6D, 0x65, 0x73, 0x70, 0x61, 
		0x63, 0x65, 0x3A, 0x49, 0x4B, 0x72, 0x69, 0x76, 0x2E, 0x57, 0x70, 0x66, 0x01, 0x00, 0x05, 
		0x00, 0x35, 0x08, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x2D, 0x01, 0x00, 0x00, 0x00, 
		0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x0C, 0x00, 0x00, 0x1D, 0xFD, 
		0x00, 0x05, 0x54, 0x69, 0x74, 0x6C, 0x65, 0x24, 0x18, 0x00, 0x00, 0x12, 0x53, 0x70, 0x72, 
		0x65, 0x61, 0x64, 0x54, 0x72, 0x61, 0x64, 0x65, 0x72, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 
		0x99, 0xFD, 0x35, 0x0A, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 
		0x03, 0x32, 0x35, 0x32, 0xA4, 0xFE, 0x36, 0x25, 0x00, 0x00, 0x00, 0x1F, 0x14, 0x01, 0x00, 
		0x1D, 0xFD, 0x00, 0x0D, 0x53, 0x69, 0x7A, 0x65, 0x54, 0x6F, 0x43, 0x6F, 0x6E, 0x74, 0x65, 
		0x6E, 0x74, 0x24, 0x0C, 0x01, 0x00, 0x06, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x3D, 0xFF, 
		0x36, 0x31, 0x00, 0x00, 0x00, 0x1F, 0x11, 0x02, 0x00, 0x1D, 0xFD, 0x00, 0x0A, 0x52, 0x65, 
		0x73, 0x69, 0x7A, 0x65, 0x4D, 0x6F, 0x64, 0x65, 0x24, 0x0E, 0x02, 0x00, 0x08, 0x4E, 0x6F, 
		0x52, 0x65, 0x73, 0x69, 0x7A, 0x65, 0x3D, 0xFF, 0x36, 0x48, 0x00, 0x00, 0x00, 0x1F, 0x0E, 
		0x03, 0x00, 0x1D, 0xFD, 0x00, 0x07, 0x54, 0x6F, 0x70, 0x6D, 0x6F, 0x73, 0x74, 0x06, 0x06, 
		0x03, 0x00, 0x2E, 0x00, 0x01, 0x2E, 0xF2, 0xFF, 0x35, 0x0B, 0x00, 0x00, 0x00, 0x06, 0x00, 
		0x00, 0x00, 0x03, 0x02, 0xFF, 0x00, 0x24, 0x0A, 0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 
		0x3D, 0xFF, 0x36, 0x0B, 0x00, 0x00, 0x00, 0x24, 0x0D, 0xCF, 0xFF, 0x07, 0x30, 0x2C, 0x30, 
		0x2C, 0x30, 0x2C, 0x30, 0x71, 0xFD, 0x36, 0x26, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 0xFF, 
		0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x37, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 
		0x03, 0x32, 0x34, 0x30, 0xA4, 0xFE, 0x36, 0x4F, 0x00, 0x00, 0x00, 0x1F, 0x1C, 0x04, 0x00, 
		0x4A, 0xFD, 0x00, 0x15, 0x52, 0x65, 0x6E, 0x64, 0x65, 0x72, 0x54, 0x72, 0x61, 0x6E, 0x73, 
		0x66, 0x6F, 0x72, 0x6D, 0x4F, 0x72, 0x69, 0x67, 0x69, 0x6E, 0x24, 0x11, 0x04, 0x00, 0x0B, 
		0x30, 0x2E, 0x35, 0x38, 0x33, 0x2C, 0x30, 0x2E, 0x34, 0x37, 0x32, 0x26, 0xFE, 0x36, 0x5B, 
		0x00, 0x00, 0x00, 0x2E, 0x2B, 0xFF, 0x35, 0x0C, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 
		0x03, 0xC9, 0xFF, 0x00, 0x2D, 0x02, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 
		0x00, 0x00, 0x00, 0x1F, 0x0B, 0x05, 0x00, 0xC9, 0xFF, 0x03, 0x04, 0x4E, 0x61, 0x6D, 0x65, 
		0x24, 0x0F, 0x05, 0x00, 0x09, 0x62, 0x74, 0x6E, 0x47, 0x6F, 0x4C, 0x6F, 0x6E, 0x67, 0x99, 
		0xFD, 0x35, 0x0C, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x24, 0x0C, 0xF2, 0xFF, 0x06, 
		0x47, 0x6F, 0x4C, 0x6F, 0x6E, 0x67, 0x99, 0xFD, 0x36, 0x24, 0x00, 0x00, 0x00, 0x24, 0x0A, 
		0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 0x3D, 0xFF, 0x36, 0x35, 0x00, 0x00, 0x00, 0x24, 
		0x0D, 0xCF, 0xFF, 0x07, 0x38, 0x2C, 0x34, 0x2C, 0x30, 0x2C, 0x30, 0x71, 0xFD, 0x36, 0x50, 
		0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 0xFF, 0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x61, 
		0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 0x03, 0x31, 0x30, 0x34, 0xA4, 0xFE, 0x36, 0x79, 
		0x00, 0x00, 0x00, 0x24, 0x08, 0xD1, 0xFF, 0x02, 0x33, 0x35, 0xA4, 0xFE, 0x36, 0x85, 0x00, 
		0x00, 0x00, 0x04, 0x03, 0xC9, 0xFF, 0x00, 0x35, 0x0D, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 
		0x00, 0x2D, 0x03, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
		0x24, 0x10, 0x05, 0x00, 0x0A, 0x62, 0x74, 0x6E, 0x52, 0x65, 0x76, 0x65, 0x72, 0x73, 0x65, 
		0x99, 0xFD, 0x35, 0x0D, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x24, 0x0D, 0xF2, 0xFF, 
		0x07, 0x52, 0x65, 0x76, 0x65, 0x72, 0x73, 0x65, 0x99, 0xFD, 0x36, 0x25, 0x00, 0x00, 0x00, 
		0x24, 0x0B, 0xD0, 0xFF, 0x05, 0x52, 0x69, 0x67, 0x68, 0x74, 0x3D, 0xFF, 0x36, 0x37, 0x00, 
		0x00, 0x00, 0x24, 0x0E, 0xCF, 0xFF, 0x08, 0x30, 0x2C, 0x34, 0x2C, 0x31, 0x32, 0x2C, 0x30, 
		0x71, 0xFD, 0x36, 0x53, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 0xFF, 0x03, 0x54, 0x6F, 0x70, 
		0x3D, 0xFF, 0x36, 0x65, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 0x03, 0x31, 0x30, 0x34, 
		0xA4, 0xFE, 0x36, 0x7D, 0x00, 0x00, 0x00, 0x24, 0x08, 0xD1, 0xFF, 0x02, 0x33, 0x35, 0xA4, 
		0xFE, 0x36, 0x89, 0x00, 0x00, 0x00, 0x04, 0x03, 0xC9, 0xFF, 0x00, 0x35, 0x0E, 0x00, 0x00, 
		0x00, 0x0A, 0x00, 0x00, 0x00, 0x2D, 0x04, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 
		0x00, 0x00, 0x00, 0x00, 0x24, 0x10, 0x05, 0x00, 0x0A, 0x62, 0x74, 0x6E, 0x47, 0x6F, 0x53, 
		0x68, 0x6F, 0x72, 0x74, 0x99, 0xFD, 0x35, 0x0E, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 
		0x24, 0x0D, 0xF2, 0xFF, 0x07, 0x47, 0x6F, 0x53, 0x68, 0x6F, 0x72, 0x74, 0x99, 0xFD, 0x36, 
		0x25, 0x00, 0x00, 0x00, 0x24, 0x0A, 0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 0x3D, 0xFF, 
		0x36, 0x37, 0x00, 0x00, 0x00, 0x24, 0x0E, 0xCF, 0xFF, 0x08, 0x38, 0x2C, 0x34, 0x36, 0x2C, 
		0x30, 0x2C, 0x30, 0x71, 0xFD, 0x36, 0x52, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 0xFF, 0x03, 
		0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x64, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 0x03, 
		0x31, 0x30, 0x34, 0xA4, 0xFE, 0x36, 0x7C, 0x00, 0x00, 0x00, 0x24, 0x08, 0xD1, 0xFF, 0x02, 
		0x33, 0x35, 0xA4, 0xFE, 0x36, 0x88, 0x00, 0x00, 0x00, 0x04, 0x03, 0xC9, 0xFF, 0x00, 0x35, 
		0x0F, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x2D, 0x05, 0x00, 0x00, 0x00, 0x35, 0x00, 
		0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x0E, 0x05, 0x00, 0x08, 0x62, 0x74, 0x6E, 
		0x43, 0x6C, 0x6F, 0x73, 0x65, 0x99, 0xFD, 0x35, 0x0F, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 
		0x00, 0x24, 0x0B, 0xF2, 0xFF, 0x05, 0x43, 0x6C, 0x6F, 0x73, 0x65, 0x99, 0xFD, 0x36, 0x23, 
		0x00, 0x00, 0x00, 0x24, 0x0B, 0xD0, 0xFF, 0x05, 0x52, 0x69, 0x67, 0x68, 0x74, 0x3D, 0xFF, 
		0x36, 0x33, 0x00, 0x00, 0x00, 0x24, 0x0F, 0xCF, 0xFF, 0x09, 0x30, 0x2C, 0x34, 0x36, 0x2C, 
		0x31, 0x32, 0x2C, 0x30, 0x71, 0xFD, 0x36, 0x4F, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 0xFF, 
		0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x62, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 
		0x03, 0x31, 0x30, 0x34, 0xA4, 0xFE, 0x36, 0x7A, 0x00, 0x00, 0x00, 0x24, 0x08, 0xD1, 0xFF, 
		0x02, 0x33, 0x35, 0xA4, 0xFE, 0x36, 0x86, 0x00, 0x00, 0x00, 0x04, 0x03, 0xC9, 0xFF, 0x00, 
		0x35, 0x10, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x2D, 0x06, 0x00, 0x00, 0x00, 0x35, 
		0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x11, 0x05, 0x00, 0x0B, 0x62, 0x74, 
		0x6E, 0x50, 0x6F, 0x73, 0x69, 0x74, 0x69, 0x6F, 0x6E, 0x99, 0xFD, 0x35, 0x10, 0x00, 0x00, 
		0x00, 0x11, 0x00, 0x00, 0x00, 0x24, 0x0E, 0xF2, 0xFF, 0x08, 0x50, 0x6F, 0x73, 0x69, 0x74, 
		0x69, 0x6F, 0x6E, 0x99, 0xFD, 0x36, 0x26, 0x00, 0x00, 0x00, 0x24, 0x0A, 0xD0, 0xFF, 0x04, 
		0x4C, 0x65, 0x66, 0x74, 0x3D, 0xFF, 0x36, 0x39, 0x00, 0x00, 0x00, 0x24, 0x0E, 0xCF, 0xFF, 
		0x08, 0x38, 0x2C, 0x38, 0x36, 0x2C, 0x30, 0x2C, 0x30, 0x71, 0xFD, 0x36, 0x54, 0x00, 0x00, 
		0x00, 0x24, 0x09, 0xC8, 0xFF, 0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x66, 0x00, 0x00, 
		0x00, 0x24, 0x09, 0xC7, 0xFF, 0x03, 0x31, 0x31, 0x30, 0xA4, 0xFE, 0x36, 0x7E, 0x00, 0x00, 
		0x00, 0x24, 0x08, 0xD1, 0xFF, 0x02, 0x33, 0x32, 0xA4, 0xFE, 0x36, 0x8A, 0x00, 0x00, 0x00, 
		0x06, 0x06, 0x7B, 0xFF, 0x2E, 0x00, 0x00, 0x04, 0x36, 0x96, 0x00, 0x00, 0x00, 0x03, 0xC9, 
		0xFF, 0x00, 0x35, 0x11, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x2D, 0x07, 0x00, 0x00, 
		0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x0E, 0x05, 0x00, 0x08, 
		0x62, 0x74, 0x6E, 0x56, 0x61, 0x6C, 0x75, 0x65, 0x99, 0xFD, 0x35, 0x11, 0x00, 0x00, 0x00, 
		0x11, 0x00, 0x00, 0x00, 0x24, 0x0B, 0xF2, 0xFF, 0x05, 0x56, 0x61, 0x6C, 0x75, 0x65, 0x99, 
		0xFD, 0x36, 0x23, 0x00, 0x00, 0x00, 0x24, 0x0B, 0xD0, 0xFF, 0x05, 0x52, 0x69, 0x67, 0x68, 
		0x74, 0x3D, 0xFF, 0x36, 0x33, 0x00, 0x00, 0x00, 0x24, 0x0F, 0xCF, 0xFF, 0x09, 0x30, 0x2C, 
		0x38, 0x36, 0x2C, 0x31, 0x32, 0x2C, 0x30, 0x71, 0xFD, 0x36, 0x4F, 0x00, 0x00, 0x00, 0x24, 
		0x09, 0xC8, 0xFF, 0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x62, 0x00, 0x00, 0x00, 0x24, 
		0x09, 0xC7, 0xFF, 0x03, 0x31, 0x31, 0x30, 0xA4, 0xFE, 0x36, 0x7A, 0x00, 0x00, 0x00, 0x24, 
		0x08, 0xD1, 0xFF, 0x02, 0x33, 0x32, 0xA4, 0xFE, 0x36, 0x86, 0x00, 0x00, 0x00, 0x06, 0x06, 
		0x7B, 0xFF, 0x2E, 0x00, 0x00, 0x04, 0x36, 0x92, 0x00, 0x00, 0x00, 0x03, 0xC9, 0xFF, 0x00, 
		0x35, 0x12, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x2D, 0x08, 0x00, 0x00, 0x00, 0x35, 
		0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x13, 0x05, 0x00, 0x0D, 0x75, 0x6E, 
		0x72, 0x65, 0x61, 0x6C, 0x69, 0x7A, 0x65, 0x64, 0x50, 0x4E, 0x4C, 0x99, 0xFD, 0x35, 0x12, 
		0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x24, 0x14, 0xF2, 0xFF, 0x0E, 0x55, 0x6E, 0x72, 
		0x65, 0x61, 0x6C, 0x69, 0x7A, 0x65, 0x64, 0x20, 0x50, 0x4E, 0x4C, 0x99, 0xFD, 0x36, 0x28, 
		0x00, 0x00, 0x00, 0x24, 0x0A, 0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 0x3D, 0xFF, 0x36, 
		0x41, 0x00, 0x00, 0x00, 0x24, 0x08, 0xD1, 0xFF, 0x02, 0x32, 0x36, 0xA4, 0xFE, 0x36, 0x5C, 
		0x00, 0x00, 0x00, 0x24, 0x0F, 0xCF, 0xFF, 0x09, 0x38, 0x2C, 0x31, 0x31, 0x38, 0x2C, 0x30, 
		0x2C, 0x30, 0x71, 0xFD, 0x36, 0x68, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 0xFF, 0x03, 0x54, 
		0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x7B, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 0x03, 0x32, 
		0x32, 0x30, 0xA4, 0xFE, 0x36, 0x93, 0x00, 0x00, 0x00, 0x06, 0x06, 0x7B, 0xFF, 0x2E, 0x00, 
		0x00, 0x04, 0x36, 0x9F, 0x00, 0x00, 0x00, 0x03, 0xF1, 0xFD, 0x00, 0x35, 0x16, 0x00, 0x00, 
		0x00, 0x0A, 0x00, 0x00, 0x00, 0x2D, 0x09, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 
		0x00, 0x00, 0x00, 0x00, 0x1F, 0x0B, 0x06, 0x00, 0xF1, 0xFD, 0x03, 0x04, 0x4E, 0x61, 0x6D, 
		0x65, 0x24, 0x11, 0x06, 0x00, 0x0B, 0x72, 0x65, 0x61, 0x6C, 0x69, 0x7A, 0x65, 0x64, 0x50, 
		0x4E, 0x4C, 0x99, 0xFD, 0x35, 0x16, 0x00, 0x00, 0x00, 0x16, 0x00, 0x00, 0x00, 0x24, 0x0A, 
		0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 0x3D, 0xFF, 0x36, 0x2B, 0x00, 0x00, 0x00, 0x24, 
		0x08, 0xD1, 0xFF, 0x02, 0x37, 0x30, 0xA4, 0xFE, 0x36, 0x46, 0x00, 0x00, 0x00, 0x24, 0x0F, 
		0xCF, 0xFF, 0x09, 0x38, 0x2C, 0x31, 0x34, 0x34, 0x2C, 0x30, 0x2C, 0x30, 0x71, 0xFD, 0x36, 
		0x52, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 0xFF, 0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 
		0x65, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 0x03, 0x32, 0x32, 0x30, 0xA4, 0xFE, 0x36, 
		0x7D, 0x00, 0x00, 0x00, 0x2E, 0x20, 0xFF, 0x35, 0x17, 0x00, 0x00, 0x00, 0x0E, 0x00, 0x00, 
		0x00, 0x03, 0x2F, 0xFF, 0x00, 0x2E, 0x54, 0xFF, 0x35, 0x18, 0x00, 0x00, 0x00, 0x12, 0x00, 
		0x00, 0x00, 0x03, 0x4A, 0xFE, 0x00, 0x2E, 0x2A, 0xFF, 0x36, 0x1C, 0x00, 0x00, 0x00, 0x2E, 
		0x2A, 0xFF, 0x35, 0x19, 0x00, 0x00, 0x00, 0x16, 0x00, 0x00, 0x00, 0x03, 0xE2, 0xFD, 0x00, 
		0x24, 0x11, 0x1E, 0xFF, 0x0B, 0x52, 0x69, 0x63, 0x68, 0x54, 0x65, 0x78, 0x74, 0x42, 0x6F, 
		0x78, 0x99, 0xFD, 0x36, 0x1A, 0x00, 0x00, 0x00, 0x04, 0x04, 0x35, 0x1A, 0x00, 0x00, 0x00, 
		0x13, 0x00, 0x00, 0x00, 0x04, 0x35, 0x1B, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x04, 
		0x35, 0x1C, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00, 0x1D, 0x26, 0x01, 0x00, 0x03, 0x00, 
		0x20, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73, 
		0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x72, 0x6F, 0x6C, 0x73, 0x2E, 0x44, 0x61, 0x74, 0x61, 0x47, 
		0x72, 0x69, 0x64, 0x03, 0x01, 0x00, 0x00, 0x35, 0x1E, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 
		0x00, 0x2D, 0x0A, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
		0x1F, 0x0B, 0x07, 0x00, 0x01, 0x00, 0x03, 0x04, 0x4E, 0x61, 0x6D, 0x65, 0x24, 0x11, 0x07, 
		0x00, 0x0B, 0x6F, 0x70, 0x65, 0x6E, 0x74, 0x69, 0x63, 0x6B, 0x65, 0x74, 0x73, 0x99, 0xFD, 
		0x35, 0x1E, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x1F, 0x18, 0x08, 0x00, 0x01, 0x00, 
		0x00, 0x11, 0x48, 0x65, 0x61, 0x64, 0x65, 0x72, 0x73, 0x56, 0x69, 0x73, 0x69, 0x62, 0x69, 
		0x6C, 0x69, 0x74, 0x79, 0x24, 0x0A, 0x08, 0x00, 0x04, 0x4E, 0x6F, 0x6E, 0x65, 0x3D, 0xFF, 
		0x36, 0x49, 0x00, 0x00, 0x00, 0x24, 0x0A, 0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 0x3D, 
		0xFF, 0x36, 0x64, 0x00, 0x00, 0x00, 0x24, 0x0F, 0xCF, 0xFF, 0x09, 0x38, 0x2C, 0x32, 0x31, 
		0x36, 0x2C, 0x30, 0x2C, 0x30, 0x71, 0xFD, 0x36, 0x7F, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 
		0xFF, 0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x92, 0x00, 0x00, 0x00, 0x24, 0x08, 0xD1, 
		0xFF, 0x02, 0x39, 0x38, 0xA4, 0xFE, 0x36, 0xAA, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC7, 0xFF, 
		0x03, 0x32, 0x32, 0x30, 0xA4, 0xFE, 0x36, 0xB6, 0x00, 0x00, 0x00, 0x1F, 0x1A, 0x09, 0x00, 
		0x01, 0x00, 0x00, 0x13, 0x41, 0x75, 0x74, 0x6F, 0x47, 0x65, 0x6E, 0x65, 0x72, 0x61, 0x74, 
		0x65, 0x43, 0x6F, 0x6C, 0x75, 0x6D, 0x6E, 0x73, 0x06, 0x06, 0x09, 0x00, 0x2E, 0x00, 0x00, 
		0x1F, 0x11, 0x0A, 0x00, 0x01, 0x00, 0x00, 0x0A, 0x49, 0x73, 0x52, 0x65, 0x61, 0x64, 0x4F, 
		0x6E, 0x6C, 0x79, 0x06, 0x06, 0x0A, 0x00, 0x2E, 0x00, 0x01, 0x07, 0xAE, 0xFF, 0x36, 0x28, 
		0x00, 0x00, 0x00, 0x03, 0xEC, 0xFF, 0x00, 0x2A, 0x10, 0x09, 0x07, 0x74, 0x69, 0x63, 0x6B, 
		0x65, 0x74, 0x73, 0x2B, 0x04, 0x08, 0x1F, 0x0E, 0x0B, 0x00, 0x01, 0x00, 0x00, 0x07, 0x43, 
		0x6F, 0x6C, 0x75, 0x6D, 0x6E, 0x73, 0x0B, 0x0B, 0x00, 0x35, 0x1F, 0x00, 0x00, 0x00, 0x0E, 
		0x00, 0x00, 0x00, 0x1D, 0x30, 0x02, 0x00, 0x03, 0x00, 0x2A, 0x53, 0x79, 0x73, 0x74, 0x65, 
		0x6D, 0x2E, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x72, 
		0x6F, 0x6C, 0x73, 0x2E, 0x44, 0x61, 0x74, 0x61, 0x47, 0x72, 0x69, 0x64, 0x54, 0x65, 0x78, 
		0x74, 0x43, 0x6F, 0x6C, 0x75, 0x6D, 0x6E, 0x03, 0x02, 0x00, 0x00, 0x35, 0x20, 0x00, 0x00, 
		0x00, 0x12, 0x00, 0x00, 0x00, 0x1D, 0x2C, 0x03, 0x00, 0x03, 0x00, 0x26, 0x53, 0x79, 0x73, 
		0x74, 0x65, 0x6D, 0x2E, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73, 0x2E, 0x43, 0x6F, 0x6E, 
		0x74, 0x72, 0x6F, 0x6C, 0x73, 0x2E, 0x44, 0x61, 0x74, 0x61, 0x47, 0x72, 0x69, 0x64, 0x43, 
		0x6F, 0x6C, 0x75, 0x6D, 0x6E, 0x1F, 0x0C, 0x0C, 0x00, 0x03, 0x00, 0x00, 0x05, 0x57, 0x69, 
		0x64, 0x74, 0x68, 0x1D, 0x35, 0x04, 0x00, 0x03, 0x00, 0x2F, 0x53, 0x79, 0x73, 0x74, 0x65, 
		0x6D, 0x2E, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x72, 
		0x6F, 0x6C, 0x73, 0x2E, 0x44, 0x61, 0x74, 0x61, 0x47, 0x72, 0x69, 0x64, 0x4C, 0x65, 0x6E, 
		0x67, 0x74, 0x68, 0x43, 0x6F, 0x6E, 0x76, 0x65, 0x72, 0x74, 0x65, 0x72, 0x24, 0x07, 0x0C, 
		0x00, 0x01, 0x2A, 0x04, 0x00, 0x36, 0x47, 0x00, 0x00, 0x00, 0x1F, 0x15, 0x0D, 0x00, 0x03, 
		0x00, 0x00, 0x0E, 0x43, 0x61, 0x6E, 0x55, 0x73, 0x65, 0x72, 0x52, 0x65, 0x6F, 0x72, 0x64, 
		0x65, 0x72, 0x06, 0x06, 0x0D, 0x00, 0x2E, 0x00, 0x00, 0x1F, 0x14, 0x0E, 0x00, 0x03, 0x00, 
		0x00, 0x0D, 0x43, 0x61, 0x6E, 0x55, 0x73, 0x65, 0x72, 0x52, 0x65, 0x73, 0x69, 0x7A, 0x65, 
		0x06, 0x06, 0x0E, 0x00, 0x2E, 0x00, 0x00, 0x1F, 0x12, 0x0F, 0x00, 0x03, 0x00, 0x00, 0x0B, 
		0x43, 0x61, 0x6E, 0x55, 0x73, 0x65, 0x72, 0x53, 0x6F, 0x72, 0x74, 0x06, 0x06, 0x0F, 0x00, 
		0x2E, 0x00, 0x00, 0x1F, 0x11, 0x10, 0x00, 0x03, 0x00, 0x00, 0x0A, 0x49, 0x73, 0x52, 0x65, 
		0x61, 0x64, 0x4F, 0x6E, 0x6C, 0x79, 0x06, 0x06, 0x10, 0x00, 0x2E, 0x00, 0x01, 0x1D, 0x31, 
		0x05, 0x00, 0x03, 0x00, 0x2B, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x57, 0x69, 0x6E, 
		0x64, 0x6F, 0x77, 0x73, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x72, 0x6F, 0x6C, 0x73, 0x2E, 0x44, 
		0x61, 0x74, 0x61, 0x47, 0x72, 0x69, 0x64, 0x42, 0x6F, 0x75, 0x6E, 0x64, 0x43, 0x6F, 0x6C, 
		0x75, 0x6D, 0x6E, 0x1F, 0x0E, 0x11, 0x00, 0x05, 0x00, 0x00, 0x07, 0x42, 0x69, 0x6E, 0x64, 
		0x69, 0x6E, 0x67, 0x07, 0x11, 0x00, 0x36, 0x25, 0x00, 0x00, 0x00, 0x03, 0xEC, 0xFF, 0x00, 
		0x2A, 0x10, 0x0F, 0x0D, 0x44, 0x69, 0x73, 0x70, 0x6C, 0x61, 0x79, 0x53, 0x74, 0x72, 0x69, 
		0x6E, 0x67, 0x2B, 0x04, 0x08, 0x04, 0x36, 0x93, 0x00, 0x00, 0x00, 0x1D, 0x34, 0x06, 0x00, 
		0x03, 0x00, 0x2E, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x57, 0x69, 0x6E, 0x64, 0x6F, 
		0x77, 0x73, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x72, 0x6F, 0x6C, 0x73, 0x2E, 0x44, 0x61, 0x74, 
		0x61, 0x47, 0x72, 0x69, 0x64, 0x54, 0x65, 0x6D, 0x70, 0x6C, 0x61, 0x74, 0x65, 0x43, 0x6F, 
		0x6C, 0x75, 0x6D, 0x6E, 0x03, 0x06, 0x00, 0x00, 0x35, 0x21, 0x00, 0x00, 0x00, 0x12, 0x00, 
		0x00, 0x00, 0x06, 0x06, 0x0E, 0x00, 0x2E, 0x00, 0x00, 0x24, 0x08, 0x0C, 0x00, 0x02, 0x37, 
		0x30, 0x04, 0x00, 0x36, 0x40, 0x00, 0x00, 0x00, 0x06, 0x06, 0x0D, 0x00, 0x2E, 0x00, 0x00, 
		0x06, 0x06, 0x0F, 0x00, 0x2E, 0x00, 0x00, 0x06, 0x06, 0x10, 0x00, 0x2E, 0x00, 0x01, 0x1F, 
		0x13, 0x12, 0x00, 0x06, 0x00, 0x00, 0x0C, 0x43, 0x65, 0x6C, 0x6C, 0x54, 0x65, 0x6D, 0x70, 
		0x6C, 0x61, 0x74, 0x65, 0x07, 0x12, 0x00, 0x35, 0x22, 0x00, 0x00, 0x00, 0x16, 0x00, 0x00, 
		0x00, 0x03, 0x88, 0xFF, 0x00, 0x35, 0x23, 0x00, 0x00, 0x00, 0x1A, 0x00, 0x00, 0x00, 0x2E, 
		0x52, 0xFF, 0x35, 0x24, 0x00, 0x00, 0x00, 0x1E, 0x00, 0x00, 0x00, 0x03, 0xC9, 0xFF, 0x00, 
		0x2D, 0x0B, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 
		0x09, 0xF2, 0xFF, 0x03, 0x44, 0x65, 0x6C, 0x99, 0xFD, 0x35, 0x24, 0x00, 0x00, 0x00, 0x40, 
		0x00, 0x00, 0x00, 0x04, 0x36, 0x51, 0x00, 0x00, 0x00, 0x04, 0x35, 0x25, 0x00, 0x00, 0x00, 
		0x1B, 0x00, 0x00, 0x00, 0x08, 0x04, 0x35, 0x27, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 
		0x0C, 0x04, 0x35, 0x29, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00, 0x03, 0x81, 0xFD, 0x00, 
		0x35, 0x2B, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x2D, 0x0C, 0x00, 0x00, 0x00, 0x35, 
		0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x0B, 0x13, 0x00, 0x81, 0xFD, 0x03, 
		0x04, 0x4E, 0x61, 0x6D, 0x65, 0x24, 0x0F, 0x13, 0x00, 0x09, 0x74, 0x78, 0x74, 0x54, 0x69, 
		0x63, 0x6B, 0x65, 0x74, 0x99, 0xFD, 0x35, 0x2B, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 
		0x24, 0x0A, 0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 0x3D, 0xFF, 0x36, 0x25, 0x00, 0x00, 
		0x00, 0x24, 0x08, 0xD1, 0xFF, 0x02, 0x32, 0x38, 0xA4, 0xFE, 0x36, 0x40, 0x00, 0x00, 0x00, 
		0x24, 0x0F, 0xCF, 0xFF, 0x09, 0x38, 0x2C, 0x33, 0x31, 0x38, 0x2C, 0x30, 0x2C, 0x30, 0x71, 
		0xFD, 0x36, 0x4C, 0x00, 0x00, 0x00, 0x1F, 0x13, 0x14, 0x00, 0x81, 0xFD, 0x00, 0x0C, 0x54, 
		0x65, 0x78, 0x74, 0x57, 0x72, 0x61, 0x70, 0x70, 0x69, 0x6E, 0x67, 0x24, 0x0A, 0x14, 0x00, 
		0x04, 0x57, 0x72, 0x61, 0x70, 0x3D, 0xFF, 0x36, 0x5F, 0x00, 0x00, 0x00, 0x24, 0x0D, 0x8E, 
		0xFF, 0x07, 0x2D, 0x31, 0x40, 0x32, 0x34, 0x35, 0x30, 0x99, 0xFD, 0x36, 0x73, 0x00, 0x00, 
		0x00, 0x24, 0x09, 0xC8, 0xFF, 0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x82, 0x00, 0x00, 
		0x00, 0x24, 0x09, 0xC7, 0xFF, 0x03, 0x31, 0x34, 0x30, 0xA4, 0xFE, 0x36, 0x9A, 0x00, 0x00, 
		0x00, 0x04, 0x03, 0xC9, 0xFF, 0x00, 0x35, 0x2C, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 
		0x2D, 0x0D, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 
		0x12, 0x05, 0x00, 0x0C, 0x62, 0x74, 0x6E, 0x41, 0x64, 0x64, 0x54, 0x69, 0x63, 0x6B, 0x65, 
		0x74, 0x99, 0xFD, 0x35, 0x2C, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x24, 0x09, 0xF2, 
		0xFF, 0x03, 0x41, 0x64, 0x64, 0x99, 0xFD, 0x36, 0x27, 0x00, 0x00, 0x00, 0x24, 0x0B, 0xD0, 
		0xFF, 0x05, 0x52, 0x69, 0x67, 0x68, 0x74, 0x3D, 0xFF, 0x36, 0x35, 0x00, 0x00, 0x00, 0x24, 
		0x10, 0xCF, 0xFF, 0x0A, 0x30, 0x2C, 0x33, 0x31, 0x38, 0x2C, 0x31, 0x32, 0x2C, 0x30, 0x71, 
		0xFD, 0x36, 0x51, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 0xFF, 0x03, 0x54, 0x6F, 0x70, 0x3D, 
		0xFF, 0x36, 0x65, 0x00, 0x00, 0x00, 0x24, 0x08, 0xC7, 0xFF, 0x02, 0x37, 0x30, 0xA4, 0xFE, 
		0x36, 0x7D, 0x00, 0x00, 0x00, 0x24, 0x08, 0xD1, 0xFF, 0x02, 0x32, 0x38, 0xA4, 0xFE, 0x36, 
		0x88, 0x00, 0x00, 0x00, 0x04, 0x36, 0x94, 0x00, 0x00, 0x00, 0x03, 0xDA, 0xFD, 0x00, 0x35, 
		0x2E, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00, 0x2D, 0x0E, 0x00, 0x00, 0x00, 0x35, 0x00, 
		0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x0B, 0x15, 0x00, 0xDA, 0xFD, 0x03, 0x04, 
		0x4E, 0x61, 0x6D, 0x65, 0x24, 0x14, 0x15, 0x00, 0x0E, 0x70, 0x67, 0x53, 0x63, 0x72, 0x6F, 
		0x6C, 0x6C, 0x56, 0x69, 0x65, 0x77, 0x65, 0x72, 0x99, 0xFD, 0x35, 0x2E, 0x00, 0x00, 0x00, 
		0x18, 0x00, 0x00, 0x00, 0x24, 0x0A, 0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 0x3D, 0xFF, 
		0x36, 0x30, 0x00, 0x00, 0x00, 0x24, 0x0A, 0x9D, 0xFF, 0x04, 0x41, 0x75, 0x74, 0x6F, 0x3D, 
		0xFF, 0x36, 0x4B, 0x00, 0x00, 0x00, 0x24, 0x0F, 0xCF, 0xFF, 0x09, 0x30, 0x2C, 0x33, 0x34, 
		0x36, 0x2C, 0x30, 0x2C, 0x30, 0x71, 0xFD, 0x36, 0x6E, 0x00, 0x00, 0x00, 0x24, 0x09, 0xC8, 
		0xFF, 0x03, 0x54, 0x6F, 0x70, 0x3D, 0xFF, 0x36, 0x81, 0x00, 0x00, 0x00, 0x24, 0x0A, 0xD1, 
		0xFF, 0x04, 0x41, 0x75, 0x74, 0x6F, 0xA4, 0xFE, 0x36, 0x99, 0x00, 0x00, 0x00, 0x24, 0x09, 
		0xCE, 0xFF, 0x03, 0x31, 0x38, 0x30, 0xA4, 0xFE, 0x36, 0xA7, 0x00, 0x00, 0x00, 0x24, 0x09, 
		0xC7, 0xFF, 0x03, 0x32, 0x32, 0x38, 0xA4, 0xFE, 0x36, 0xB7, 0x00, 0x00, 0x00, 0x2E, 0xF2, 
		0xFF, 0x35, 0x30, 0x00, 0x00, 0x00, 0x0E, 0x00, 0x00, 0x00, 0x1D, 0x3A, 0x07, 0x00, 0x05, 
		0x00, 0x34, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 
		0x73, 0x2E, 0x43, 0x6F, 0x6E, 0x74, 0x72, 0x6F, 0x6C, 0x73, 0x2E, 0x57, 0x70, 0x66, 0x50, 
		0x72, 0x6F, 0x70, 0x65, 0x72, 0x74, 0x79, 0x47, 0x72, 0x69, 0x64, 0x2E, 0x50, 0x72, 0x6F, 
		0x70, 0x65, 0x72, 0x74, 0x79, 0x47, 0x72, 0x69, 0x64, 0x03, 0x07, 0x00, 0x00, 0x2D, 0x0F, 
		0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x0B, 0x16, 
		0x00, 0x07, 0x00, 0x03, 0x04, 0x4E, 0x61, 0x6D, 0x65, 0x24, 0x10, 0x16, 0x00, 0x0A, 0x50, 
		0x61, 0x72, 0x61, 0x6D, 0x65, 0x74, 0x65, 0x72, 0x73, 0x99, 0xFD, 0x35, 0x30, 0x00, 0x00, 
		0x00, 0x1E, 0x00, 0x00, 0x00, 0x24, 0x0A, 0xD0, 0xFF, 0x04, 0x4C, 0x65, 0x66, 0x74, 0x3D, 
		0xFF, 0x36, 0x33, 0x00, 0x00, 0x00, 0x07, 0xC7, 0xFF, 0x35, 0x31, 0x00, 0x00, 0x00, 0x15, 
		0x00, 0x00, 0x00, 0x03, 0xEC, 0xFF, 0x00, 0x1F, 0x12, 0x17, 0x00, 0xEC, 0xFF, 0x00, 0x0B, 
		0x45, 0x6C, 0x65, 0x6D, 0x65, 0x6E, 0x74, 0x4E, 0x61, 0x6D, 0x65, 0x24, 0x14, 0x17, 0x00, 
		0x0E, 0x70, 0x67, 0x53, 0x63, 0x72, 0x6F, 0x6C, 0x6C, 0x56, 0x69, 0x65, 0x77, 0x65, 0x72, 
		0x99, 0xFD, 0x1F, 0x0B, 0x18, 0x00, 0xEC, 0xFF, 0x00, 0x04, 0x50, 0x61, 0x74, 0x68, 0x24, 
		0x11, 0x18, 0x00, 0x0B, 0x41, 0x63, 0x74, 0x75, 0x61, 0x6C, 0x57, 0x69, 0x64, 0x74, 0x68, 
		0x14, 0xFE, 0x1F, 0x10, 0x19, 0x00, 0xEC, 0xFF, 0x00, 0x09, 0x43, 0x6F, 0x6E, 0x76, 0x65, 
		0x72, 0x74, 0x65, 0x72, 0x07, 0x19, 0x00, 0x1D, 0x1D, 0x08, 0x00, 0x00, 0x00, 0x17, 0x49, 
		0x4B, 0x72, 0x69, 0x76, 0x2E, 0x57, 0x70, 0x66, 0x2E, 0x4D, 0x61, 0x74, 0x68, 0x43, 0x6F, 
		0x6E, 0x76, 0x65, 0x72, 0x74, 0x65, 0x72, 0x03, 0x08, 0x00, 0x00, 0x04, 0x08, 0x1F, 0x19, 
		0x1A, 0x00, 0xEC, 0xFF, 0x00, 0x12, 0x43, 0x6F, 0x6E, 0x76, 0x65, 0x72, 0x74, 0x65, 0x72, 
		0x50, 0x61, 0x72, 0x61, 0x6D, 0x65, 0x74, 0x65, 0x72, 0x24, 0x0A, 0x1A, 0x00, 0x04, 0x78, 
		0x2D, 0x31, 0x30, 0x99, 0xFD, 0x04, 0x08, 0x04, 0x35, 0x33, 0x00, 0x00, 0x00, 0x0F, 0x00, 
		0x00, 0x00, 0x04, 0x35, 0x34, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x04, 0x35, 0x35, 
		0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x04, 0x35, 0x39, 0x00, 0x00, 0x00, 0x03, 0x00, 
		0x00, 0x00, 0x02, 
	};
		}
	}