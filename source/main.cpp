#include "original.h"

#ifdef __cplusplus
#define DLLEXPORT extern "C" __declspec(dllexport)
#else
#define DLLEXPORT __declspec(dllexport)
#endif

DLLEXPORT void __stdcall view(short* x, short* y, short* z, unsigned char* r, unsigned char* g, unsigned char* b, int count)
{
	pcl::PointCloud<pcl::PointXYZRGBA>::Ptr p_cloud(new pcl::PointCloud<pcl::PointXYZRGBA>);
	//pcl::PointCloud<pcl::Normal>::Ptr normals(new pcl::PointCloud<pcl::Normal>);
	original::make_cloud_XYZRGBA(p_cloud, count, x, y, z, r, g, b, 0.01);
	original::launch_viewer_XYZRGBA(p_cloud);
}

//ICP Registration Gray
//NaNÇ‚0ÇÕC#ÇÃï˚Ç≈èúÇ¢ÇƒÇ®Ç≠
DLLEXPORT void __stdcall match_XYZ(short* x1, short* y1, short* z1, short* x2, short* y2, short* z2, short** x3, short** y3, short** z3, int count1, int count2, int* count3, double** transMat, double magnification, int maximumIterations, float leafsize, bool view)
{
	pcl::PointCloud<pcl::PointXYZ>::Ptr source(new pcl::PointCloud<pcl::PointXYZ>());
	pcl::PointCloud<pcl::PointXYZ>::Ptr target(new pcl::PointCloud<pcl::PointXYZ>());
	original::make_cloud_XYZ(source, count1, x1, y1, z1, magnification);
	original::make_cloud_XYZ(target, count2, x2, y2, z2, magnification);
	if (maximumIterations > 0) original::register_by_icp_XYZ(source, target, transMat, maximumIterations);
	else *transMat = new double[16]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
	pcl::PointCloud<pcl::PointXYZRGBA>::Ptr result_XYZRGBA(new pcl::PointCloud<pcl::PointXYZRGBA>());
	original::merge_clouds_XYZ_to_XYZRGBA(source, target, result_XYZRGBA);
	if (leafsize > 0) original::down_sampling_XYZRGBA(result_XYZRGBA, leafsize);
	pcl::PointCloud<pcl::PointXYZ>::Ptr result_XYZ(new pcl::PointCloud<pcl::PointXYZ>());
	original::XYZRGBA_to_XYZ(result_XYZRGBA, result_XYZ);
	original::return_pointer_XYZ(result_XYZ, x3, y3, z3, count3);
	if (view) original::launch_viewer_XYZRGBA(result_XYZRGBA);
}

//ICP Registration Color
//NaNÇ‚0ÇÕC#ÇÃï˚Ç≈èúÇ¢ÇƒÇ®Ç≠
DLLEXPORT void __stdcall match_XYZRGB(short* x1, short* y1, short* z1, unsigned char* r1, unsigned char* g1, unsigned char* b1, 
	short* x2, short* y2, short* z2, unsigned char* r2, unsigned char* g2, unsigned char* b2, short** x3, short** y3, short** z3, unsigned char** r3, unsigned char** g3, unsigned char** b3, 
	int count1, int count2, int* count3, double** transMat, double magnification, int maximumIterations, float leafsize, bool view)
{
	pcl::PointCloud<pcl::PointXYZRGBA>::Ptr source(new pcl::PointCloud<pcl::PointXYZRGBA>());
	pcl::PointCloud<pcl::PointXYZRGBA>::Ptr target(new pcl::PointCloud<pcl::PointXYZRGBA>());
	original::make_cloud_XYZRGBA(source, count1, x1, y1, z1, r1, g1, b1, magnification);
	original::make_cloud_XYZRGBA(target, count2, x2, y2, z2, r2, g2, b2, magnification);
	if (maximumIterations > 0) original::register_by_icp_XYZRGBA(source, target, transMat, maximumIterations);
	else *transMat = new double[16]{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
	pcl::PointCloud<pcl::PointXYZRGBA>::Ptr result_XYZRGBA(new pcl::PointCloud<pcl::PointXYZRGBA>());
	original::merge_clouds_XYZRGBA_to_XYZRGBA(source, target, result_XYZRGBA);
	if (leafsize > 0) original::down_sampling_XYZRGBA(result_XYZRGBA, leafsize);
	original::return_pointer_XYZRGB(result_XYZRGBA, x3, y3, z3, r3, g3, b3, count3);
	if (view) original::launch_viewer_XYZRGBA(result_XYZRGBA);
}
