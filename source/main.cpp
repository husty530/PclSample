#include <pcl/visualization/cloud_viewer.h>
#include <pcl/features/integral_image_normal.h>
#include <pcl/features/fpfh.h>
#include <pcl/features/normal_3d.h>
#include <pcl/filters/voxel_grid.h>
#include <iostream>

#ifdef __cplusplus
#define DLLEXPORT extern "C" __declspec(dllexport)
#else
#define DLLEXPORT __declspec(dllexport)
#endif
using namespace std;


DLLEXPORT void __stdcall view(short* x, short* y, short* z, unsigned char* r, unsigned char* g, unsigned char* b, int width, int height)
{
	pcl::PointCloud<pcl::PointXYZRGBA>::Ptr p_cloud(new pcl::PointCloud<pcl::PointXYZRGBA>);
	pcl::PointCloud<pcl::Normal>::Ptr normals(new pcl::PointCloud<pcl::Normal>);
	p_cloud->width = width;
	p_cloud->height = height;
	p_cloud->points.resize(width*height);
	p_cloud->is_dense = false;

	for (int i = 0; i < width * height; i++) {
		pcl::PointXYZRGBA& point = p_cloud->points[i];
		point.x = x[i]*0.01;
		point.y = -y[i]*0.01;
		point.z = -z[i]*0.01;
		point.r = r[i];
		point.g = g[i];
		point.b = b[i];
		point.a = 255;
	}
	
	pcl::visualization::PCLVisualizer viewer("PointCloudViewer");
	viewer.setBackgroundColor(0.2, 0.2, 0.2);
	viewer.addPointCloud<pcl::PointXYZRGBA>(p_cloud, "cloud");

	while (!viewer.wasStopped())
	{
		viewer.spinOnce(100);
	}
}