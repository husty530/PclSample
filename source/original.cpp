#include "original.h"

void original::make_cloud_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud, int count, short* x, short* y, short* z, double magnification)
{
	cloud->width = count;
	cloud->height = 1;
	cloud->points.resize(count);
	cloud->is_dense = false;
	for (int i = 0; i < count; i++) {
		pcl::PointXYZ& point = cloud->points[i];
		point.x = x[i] * magnification;
		point.y = y[i] * magnification;
		point.z = z[i] * magnification;
	}
}

void original::make_cloud_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud, int count, short* x, short* y, short* z,
	unsigned char* r, unsigned char* g, unsigned char* b, double magnification)
{
	cloud->width = count;
	cloud->height = 1;
	cloud->points.resize(count);
	cloud->is_dense = false;
	for (int i = 0; i < count; i++) {
		pcl::PointXYZRGBA& point = cloud->points[i];
		point.x = x[i] * magnification;
		point.y = y[i] * magnification;
		point.z = z[i] * magnification;
		point.r = r[i];
		point.g = g[i];
		point.b = b[i];
		point.a = 255;
	}
}

void original::XYZ_to_XYZRGBA(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud_XYZ, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud_XYZRGBA)
{
	cloud_XYZRGBA->width = cloud_XYZ->size();
	cloud_XYZRGBA->height = 1;
	cloud_XYZRGBA->points.resize(cloud_XYZ->size());
	cloud_XYZRGBA->is_dense = false;
	for (int i = 0; i < cloud_XYZRGBA->size(); i++) {
		pcl::PointXYZ& p1 = cloud_XYZ->points[i];
		pcl::PointXYZRGBA& p2 = cloud_XYZRGBA->points[i];
		p2.x = p1.x;
		p2.y = p1.y;
		p2.z = p1.z;
		p2.r = 255;
		p2.g = 255;
		p2.b = 255;
		p2.a = 255;
	}
}

void original::XYZRGBA_to_XYZ(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud_XYZRGBA, pcl::PointCloud<pcl::PointXYZ>::Ptr cloud_XYZ)
{
	cloud_XYZ->width = cloud_XYZRGBA->size();
	cloud_XYZ->height = 1;
	cloud_XYZ->points.resize(cloud_XYZRGBA->size());
	cloud_XYZ->is_dense = false;
	for (int i = 0; i < cloud_XYZ->size(); i++) {
		pcl::PointXYZRGBA& p1 = cloud_XYZRGBA->points[i];
		pcl::PointXYZ& p2 = cloud_XYZ->points[i];
		p2.x = p1.x;
		p2.y = p1.y;
		p2.z = p1.z;
	}
}

void original::launch_viewer_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud)
{
	pcl::visualization::PCLVisualizer viewer("PointCloudViewer");
	viewer.setBackgroundColor(0, 0, 0);
	viewer.addPointCloud<pcl::PointXYZ>(cloud, "cloud");
	while (!viewer.wasStopped())
	{
		viewer.spinOnce(100);
	}
}

void original::launch_viewer_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud)
{
	pcl::visualization::PCLVisualizer viewer("PointCloudViewer");
	viewer.setBackgroundColor(0, 0, 0);
	viewer.addPointCloud<pcl::PointXYZRGBA>(cloud, "cloud");
	
	while (!viewer.wasStopped())
	{
		viewer.spinOnce(100);
	}
}

void original::merge_clouds_XYZ_to_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr input1, pcl::PointCloud<pcl::PointXYZ>::Ptr input2, pcl::PointCloud<pcl::PointXYZ>::Ptr output)
{
	output->width = input1->size() + input2->size();
	output->height = 1;
	output->points.resize(output->width);
	output->is_dense = false;
	for (int i = 0; i < input1->width; i++) {
		pcl::PointXYZ& p1 = input1->points[i];
		pcl::PointXYZ& p2 = output->points[i];
		p2.x = p1.x;
		p2.y = p1.y;
		p2.z = p1.z;
	}
	for (int i = 0; i < input2->width; i++)
	{
		pcl::PointXYZ& p3 = input2->points[i];
		pcl::PointXYZ& p4 = output->points[i + input1->width];
		p4.x = p3.x;
		p4.y = p3.y;
		p4.z = p3.z;
	}
}
void original::merge_clouds_XYZ_to_XYZRGBA(pcl::PointCloud<pcl::PointXYZ>::Ptr input1, pcl::PointCloud<pcl::PointXYZ>::Ptr input2, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr output)
{
	output->width = input1->size() + input2->size();
	output->height = 1;
	output->points.resize(output->width);
	output->is_dense = false;
	for (int i = 0; i < input1->width; i++) {
		pcl::PointXYZ& p1 = input1->points[i];
		pcl::PointXYZRGBA& p2 = output->points[i];
		p2.x = p1.x;
		p2.y = p1.y;
		p2.z = p1.z;
		p2.r = 200;
		p2.g = 50;
		p2.b = 150;
		p2.a = 255;
	}
	for (int i = 0; i < input2->width; i++)
	{
		pcl::PointXYZ& p3 = input2->points[i];
		pcl::PointXYZRGBA& p4 = output->points[i + input1->width];
		p4.x = p3.x;
		p4.y = p3.y;
		p4.z = p3.z;
		p4.r = 0;
		p4.g = 255;
		p4.b = 0;
		p4.a = 255;
	}
}

void original::merge_clouds_XYZRGBA_to_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr input1, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr input2, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr output)
{
	output->width = input1->size() + input2->size();
	output->height = 1;
	output->points.resize(output->width);
	output->is_dense = false;
	for (int i = 0; i < input1->width; i++) {
		pcl::PointXYZRGBA& p1 = input1->points[i];
		pcl::PointXYZRGBA& p2 = output->points[i];
		p2.x = p1.x;
		p2.y = p1.y;
		p2.z = p1.z;
		p2.r = p1.r;
		p2.g = p1.g;
		p2.b = p1.b;
		p2.a = p1.a;
	}
	for (int i = 0; i < input2->width; i++)
	{
		pcl::PointXYZRGBA& p3 = input2->points[i];
		pcl::PointXYZRGBA& p4 = output->points[i + input1->width];
		p4.x = p3.x;
		p4.y = p3.y;
		p4.z = p3.z;
		p4.r = p3.r;
		p4.g = p3.g;
		p4.b = p3.b;
		p4.a = p3.a;
	}
}

void original::down_sampling_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud, int leafsize)
{
	pcl::PointCloud<pcl::PointXYZ>::Ptr tmp(new pcl::PointCloud<pcl::PointXYZ>);
	pcl::VoxelGrid<pcl::PointXYZ> vg;
	vg.setInputCloud(cloud);
	vg.setLeafSize(leafsize, leafsize, leafsize);
	vg.filter(*tmp);
	*cloud = *tmp;
}

void original::down_sampling_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud, int leafsize)
{
	pcl::PointCloud<pcl::PointXYZRGBA>::Ptr tmp(new pcl::PointCloud<pcl::PointXYZRGBA>);
	pcl::VoxelGrid<pcl::PointXYZRGBA> vg;
	vg.setInputCloud(cloud);
	vg.setLeafSize(leafsize, leafsize, leafsize);
	vg.filter(*tmp);
	*cloud = *tmp;
}

void original::register_by_icp_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr input_output, pcl::PointCloud<pcl::PointXYZ>::Ptr target, double** transformation_array, int maximum_iterations)
{
	pcl::IterativeClosestPoint<pcl::PointXYZ, pcl::PointXYZ> icp;
	icp.setInputSource(input_output);
	icp.setInputTarget(target);
	icp.setMaximumIterations(maximum_iterations);
	icp.setMaxCorrespondenceDistance(100);
	icp.align(*input_output);
	Eigen::Matrix4d m = Eigen::Matrix4d::Identity();
	m = icp.getFinalTransformation().cast<double>();
	double* d = new double[16];
	for (int h = 0; h < 4; h++)
	{
		for (int w = 0; w < 4; w++)
		{
			d[h * 4 + w] = m(h, w);
		}
	}
	*transformation_array = d;
}

void original::register_by_icp_XYZRGBA(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr input_output, pcl::PointCloud<pcl::PointXYZRGBA>::Ptr target, double** transformation_array, int maximum_iterations)
{
	pcl::IterativeClosestPoint<pcl::PointXYZRGBA, pcl::PointXYZRGBA> icp;
	icp.setInputSource(input_output);
	icp.setInputTarget(target);
	icp.setMaximumIterations(maximum_iterations);
	icp.setMaxCorrespondenceDistance(100);
	icp.align(*input_output);
	Eigen::Matrix4d m = Eigen::Matrix4d::Identity();
	m = icp.getFinalTransformation().cast<double>();
	double* d = new double[16];
	for (int h = 0; h < 4; h++)
	{
		for (int w = 0; w < 4; w++)
		{
			d[h * 4 + w] = m(h, w);
		}
	}
	*transformation_array = d;
}

void original::return_pointer_XYZ(pcl::PointCloud<pcl::PointXYZ>::Ptr cloud_XYZ, short** x, short** y, short** z, int* count)
{
	short* tx = new short[cloud_XYZ->size()];
	short* ty = new short[cloud_XYZ->size()];
	short* tz = new short[cloud_XYZ->size()];
	for (int i = 0; i < cloud_XYZ->size(); i++)
	{
		pcl::PointXYZ& p = cloud_XYZ->points[i];
		tx[i] = (short)p.x;
		ty[i] = (short)p.y;
		tz[i] = (short)p.z;
	}
	*x = tx;
	*y = ty;
	*z = tz;
	int tcount = cloud_XYZ->size();
	*count = tcount;
}

void original::return_pointer_XYZRGB(pcl::PointCloud<pcl::PointXYZRGBA>::Ptr cloud_XYZRGBA, short** x, short** y, short** z, unsigned char** r, unsigned char** g, unsigned char** b, int* count)
{
	short* tx = new short[cloud_XYZRGBA->size()];
	short* ty = new short[cloud_XYZRGBA->size()];
	short* tz = new short[cloud_XYZRGBA->size()];
	unsigned char* tr = new unsigned char[cloud_XYZRGBA->size()];
	unsigned char* tg = new unsigned char[cloud_XYZRGBA->size()];
	unsigned char* tb = new unsigned char[cloud_XYZRGBA->size()];
	for (int i = 0; i < cloud_XYZRGBA->size(); i++)
	{
		pcl::PointXYZRGBA& p = cloud_XYZRGBA->points[i];
		tx[i] = (short)p.x;
		ty[i] = (short)p.y;
		tz[i] = (short)p.z;
		tr[i] = p.r;
		tg[i] = p.g;
		tb[i] = p.b;
	}
	*x = tx;
	*y = ty;
	*z = tz;
	*r = tr;
	*g = tg;
	*b = tb;
	int tcount = cloud_XYZRGBA->size();
	*count = tcount;
}

