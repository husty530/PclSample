#pragma once
#include <pcl/visualization/cloud_viewer.h>
#include <pcl/features/integral_image_normal.h>
#include <pcl/features/fpfh.h>
#include <pcl/features/normal_3d.h>
#include <pcl/filters/voxel_grid.h>
#include <pcl/registration/icp.h>

/// <summary>
/// PointCloudの作成・表示など共通処理を行うクラス
/// </summary>
static class original
{
public:

	static void make_cloud_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud, int count, short* x, short* y, short* z, double magnification);
	static void make_cloud_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud, int count, short* x, short* y, short* z, unsigned char* r, unsigned char* g, unsigned char* b, double magnification);
	
	static void XYZ_to_XYZRGBA(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud_XYZ, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud_XYZRGBA);
	static void XYZRGBA_to_XYZ(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud_XYZRGBA, pcl::PointCloud<pcl::PointXYZ>::Ptr cloud_XYZ);

	static void launch_viewer_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud);
	static void launch_viewer_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud);

	static void merge_clouds_XYZ_to_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr input1, pcl::PointCloud<pcl::PointXYZ>::Ptr input2, pcl::PointCloud<pcl::PointXYZ>::Ptr output);
	static void merge_clouds_XYZ_to_XYZRGBA(pcl::PointCloud<pcl::PointXYZ>::Ptr input1, pcl::PointCloud<pcl::PointXYZ>::Ptr input2, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr output);
	static void merge_clouds_XYZRGBA_to_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr input1, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr input2, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr output);

	static void down_sampling_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud, int leafsize);
	static void down_sampling_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud, int leafsize);

	static void register_by_icp_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr input_output, pcl::PointCloud<pcl::PointXYZ>::Ptr target, double** transformation_array, int maximum_iterations);
	static void register_by_icp_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr input_output, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr target, double** transformation_array, int maximum_iterations);

	static void return_pointer_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud_XYZ, short** x, short** y, short** z, int* count);
	static void return_pointer_XYZRGB(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud_XYZRGBA, short** x, short** y, short** z, unsigned char** r, unsigned char** g, unsigned char** b, int* count);

};

